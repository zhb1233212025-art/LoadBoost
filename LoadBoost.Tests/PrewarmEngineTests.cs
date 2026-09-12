using System;
using System.IO;
using System.Linq;
using System.Threading;
using LoadBoost.Core;
using Xunit;

namespace LoadBoost.Tests
{
    public sealed class PrewarmEngineTests : IDisposable
    {
        private readonly string _root;

        public PrewarmEngineTests()
        {
            _root = Path.Combine(Path.GetTempPath(), "loadboost_prewarm_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_root);
            File.WriteAllBytes(Path.Combine(_root, "a.bin"), new byte[100000]);
            File.WriteAllBytes(Path.Combine(_root, "b.bin"), new byte[1500000]);
            File.WriteAllBytes(Path.Combine(_root, "c.bin"), new byte[0]);
        }

        public void Dispose()
        {
            try { Directory.Delete(_root, true); } catch { }
        }

        [Fact]
        public void Start_ReadsAllFiles()
        {
            var files = Directory.GetFiles(_root);
            long expected = files.Sum(f => new FileInfo(f).Length);
            var engine = new PrewarmEngine(files, 2);
            engine.Start();
            Assert.True(SpinWait.SpinUntil(() => engine.Finished, TimeSpan.FromSeconds(30)));
            engine.Stop();
            Assert.Equal(expected, engine.Stats.BytesRead);
            Assert.Equal(files.Length, engine.Stats.FilesRead);
            Assert.Equal(0, engine.Stats.FilesFailed);
        }

        [Fact]
        public void Start_ToleratesMissingFiles()
        {
            var files = Directory.GetFiles(_root).Concat(new[] { Path.Combine(_root, "nope.bin") });
            var engine = new PrewarmEngine(files, 2);
            engine.Start();
            Assert.True(SpinWait.SpinUntil(() => engine.Finished, TimeSpan.FromSeconds(30)));
            engine.Stop();
            Assert.Equal(1, engine.Stats.FilesFailed);
            Assert.Equal(3, engine.Stats.FilesRead);
        }
    }
}
