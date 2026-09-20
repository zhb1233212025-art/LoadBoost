using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoadBoost.Core
{
    public static class FileScanner
    {
        public static List<ModDiskStats> Scan(string gameDataRoot)
        {
            if (!Directory.Exists(gameDataRoot))
                throw new DirectoryNotFoundException("GameData not found: " + gameDataRoot);

            var result = new List<ModDiskStats>();
            foreach (var topDir in SafeEnumerateDirs(gameDataRoot))
            {
                var stats = new ModDiskStats { ModName = Path.GetFileName(topDir) };
                Accumulate(stats, topDir);
                if (stats.FileCount > 0) result.Add(stats);
            }
            var loose = new ModDiskStats { ModName = "(GameData 根目录散文件)" };
            AccumulateTopOnly(loose, gameDataRoot);
            if (loose.FileCount > 0) result.Add(loose);
            return result.OrderByDescending(s => s.TotalBytes).ToList();
        }

        // 递归累加某 mod 目录;逐层降级,单个坏目录不拖垮整盘扫描;跳过 junction/reparse 防循环。
        private static void Accumulate(ModDiskStats stats, string dir)
        {
            foreach (var file in SafeEnumerateFiles(dir))
            {
                AddFileSafe(stats, file);
            }
            foreach (var sub in SafeEnumerateDirs(dir))
            {
                Accumulate(stats, sub);
            }
        }

        private static void AccumulateTopOnly(ModDiskStats stats, string dir)
        {
            foreach (var file in SafeEnumerateFiles(dir))
            {
                AddFileSafe(stats, file);
            }
        }

        private static void AddFileSafe(ModDiskStats stats, string file)
        {
            long len;
            try { len = new FileInfo(file).Length; }
            catch (Exception) { return; } // 扫描途中文件消失,跳过
            AddFile(stats, file, len);
        }

        // 枚举文件,枚举途中异常(权限/目录消失/路径过长)逐次吞掉,不中断
        private static IEnumerable<string> SafeEnumerateFiles(string dir)
        {
            IEnumerable<string> files = null;
            try { files = Directory.EnumerateFiles(dir, "*", SearchOption.TopDirectoryOnly); }
            catch (Exception) { yield break; }
            using (var e = files.GetEnumerator())
            {
                while (true)
                {
                    string cur = null;
                    bool ok = false;
                    try { ok = e.MoveNext(); if (ok) cur = e.Current; }
                    catch (Exception) { yield break; }
                    if (!ok) yield break;
                    yield return cur;
                }
            }
        }

        // 枚举子目录,跳过 reparse point(junction/符号链接),防递归循环
        private static IEnumerable<string> SafeEnumerateDirs(string dir)
        {
            IEnumerable<string> dirs = null;
            try { dirs = Directory.EnumerateDirectories(dir, "*", SearchOption.TopDirectoryOnly); }
            catch (Exception) { yield break; }
            using (var e = dirs.GetEnumerator())
            {
                while (true)
                {
                    string cur = null;
                    bool ok = false;
                    try { ok = e.MoveNext(); if (ok) cur = e.Current; }
                    catch (Exception) { yield break; }
                    if (!ok) yield break;
                    bool isReparse = false;
                    try
                    {
                        isReparse = (File.GetAttributes(cur) & FileAttributes.ReparsePoint) != 0;
                    }
                    catch (Exception) { }
                    if (!isReparse) yield return cur;
                }
            }
        }

        internal static void AddFile(ModDiskStats stats, string path, long len)
        {
            stats.FileCount++;
            stats.TotalBytes += len;
            switch (Path.GetExtension(path).ToLowerInvariant())
            {
                case ".png": case ".dds": case ".tga": case ".mbm": case ".jpg": case ".jpeg":
                    stats.TextureBytes += len; break;
                case ".mu":
                    stats.ModelBytes += len; break;
                case ".wav": case ".ogg":
                    stats.AudioBytes += len; break;
                case ".cfg":
                    stats.ConfigBytes += len; break;
                default:
                    stats.OtherBytes += len; break;
            }
        }
    }
}
