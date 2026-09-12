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
            foreach (var topDir in Directory.EnumerateDirectories(gameDataRoot))
            {
                var stats = new ModDiskStats { ModName = Path.GetFileName(topDir) };
                Accumulate(stats, topDir, SearchOption.AllDirectories);
                if (stats.FileCount > 0) result.Add(stats);
            }
            var loose = new ModDiskStats { ModName = "(GameData 根目录散文件)" };
            Accumulate(loose, gameDataRoot, SearchOption.TopDirectoryOnly);
            if (loose.FileCount > 0) result.Add(loose);
            return result.OrderByDescending(s => s.TotalBytes).ToList();
        }

        private static void Accumulate(ModDiskStats stats, string dir, SearchOption option)
        {
            foreach (var file in Directory.EnumerateFiles(dir, "*", option))
            {
                long len;
                try { len = new FileInfo(file).Length; }
                catch (Exception) { continue; } // 扫描途中文件消失,跳过
                AddFile(stats, file, len);
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
