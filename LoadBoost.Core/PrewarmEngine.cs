using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace LoadBoost.Core
{
    public sealed class PrewarmEngine
    {
        private const int BufferSize = 1024 * 1024;
        private readonly IEnumerable<string> _files;
        private readonly ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();
        private readonly int _threadCount;
        private readonly List<Thread> _threads = new List<Thread>();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly Stopwatch _watch = new Stopwatch();
        private volatile bool _producerDone;
        private long _filesRead;
        private long _bytesRead;
        private long _filesFailed;
        private bool _started;

        public PrewarmEngine(IEnumerable<string> files, int threadCount)
        {
            // 延迟枚举:目录遍历发生在生产者线程,不阻塞调用方(Unity 主线程)
            _files = files ?? Enumerable.Empty<string>();
            _threadCount = Math.Max(1, threadCount);
        }

        public bool Finished => _started && _threads.All(t => !t.IsAlive);

        public PrewarmStats Stats => new PrewarmStats
        {
            FilesRead = Interlocked.Read(ref _filesRead),
            BytesRead = Interlocked.Read(ref _bytesRead),
            FilesFailed = Interlocked.Read(ref _filesFailed),
            Elapsed = _watch.Elapsed
        };

        public void Start()
        {
            if (_started) return;
            _started = true;
            _watch.Start();
            var producer = new Thread(Produce) { IsBackground = true, Name = "LoadBoost.Prewarm.Producer" };
            _threads.Add(producer);
            producer.Start();
            for (int i = 0; i < _threadCount; i++)
            {
                var t = new Thread(Work) { IsBackground = true, Name = "LoadBoost.Prewarm" };
                _threads.Add(t);
                t.Start();
            }
        }

        public void Stop()
        {
            _cts.Cancel();
            foreach (var t in _threads) t.Join(2000);
            _watch.Stop();
        }

        private void Produce()
        {
            try
            {
                foreach (var f in _files)
                {
                    if (_cts.IsCancellationRequested) break;
                    _queue.Enqueue(f);
                }
            }
            catch (Exception)
            {
                // 枚举失败(目录消失/权限):已入队的文件照常预热
            }
            finally
            {
                _producerDone = true;
            }
        }

        private void Work()
        {
            try
            {
                var buffer = new byte[BufferSize];
                while (!_cts.IsCancellationRequested)
                {
                    string path;
                    if (_queue.TryDequeue(out path))
                    {
                        ReadFile(path, buffer);
                    }
                    else if (_producerDone)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
                }
            }
            catch (Exception)
            {
                // 线程级兜底:绝不允许未捕获异常波及游戏进程
            }
        }

        private void ReadFile(string path, byte[] buffer)
        {
            try
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read,
                    FileShare.ReadWrite | FileShare.Delete, BufferSize, FileOptions.SequentialScan))
                {
                    while (!_cts.IsCancellationRequested)
                    {
                        int n = fs.Read(buffer, 0, buffer.Length);
                        if (n <= 0) break;
                        Interlocked.Add(ref _bytesRead, n);
                    }
                }
                Interlocked.Increment(ref _filesRead);
            }
            catch (Exception)
            {
                // 锁定/权限/消失的文件:计数后跳过,绝不让线程死掉
                Interlocked.Increment(ref _filesFailed);
            }
        }
    }
}
