using System;
using System.IO;
using System.Linq;
using LoadBoost.Core;
using Xunit;

namespace LoadBoost.Tests
{
    public sealed class FileScannerTests : IDisposable
    {
        private readonly string _root;

        public FileScannerTests()
        {
            _root = Path.Combine(Path.GetTempPath(), "loadboost_scan_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(Path.Combine(_root, "ModA", "Sub"));
            Directory.CreateDirectory(Path.Combine(_root, "ModB"));
            File.WriteAllBytes(Path.Combine(_root, "ModA", "tex.dds"), new byte[2000]);
            File.WriteAllBytes(Path.Combine(_root, "ModA", "Sub", "part.mu"), new byte[3000]);
            File.WriteAllBytes(Path.Combine(_root, "ModB", "sound.wav"), new byte[5000]);
            File.WriteAllBytes(Path.Combine(_root, "ModB", "settings.cfg"), new byte[100]);
            File.WriteAllBytes(Path.Combine(_root, "loose.dll"), new byte[50]);
        }

        public void Dispose()
        {
            try { Directory.Delete(_root, true); } catch { }
        }

        [Fact]
        public void Scan_GroupsByTopFolder_AndClassifies()
        {
            var stats = FileScanner.Scan(_root);
            var a = stats.Single(s => s.ModName == "ModA");
            Assert.Equal(5000, a.TotalBytes);
            Assert.Equal(2000, a.TextureBytes);
            Assert.Equal(3000, a.ModelBytes);
            Assert.Equal(2, a.FileCount);
            var b = stats.Single(s => s.ModName == "ModB");
            Assert.Equal(5000, b.AudioBytes);
            Assert.Equal(100, b.ConfigBytes);
            Assert.Contains(stats, s => s.ModName == "(GameData 根目录散文件)" && s.OtherBytes == 50);
        }

        [Fact]
        public void Scan_SortsByTotalBytesDescending()
        {
            var stats = FileScanner.Scan(_root);
            Assert.Equal("ModB", stats[0].ModName);
        }

        [Fact]
        public void Scan_ThrowsOnMissingDirectory()
        {
            Assert.Throws<DirectoryNotFoundException>(() => FileScanner.Scan(Path.Combine(_root, "nope")));
        }
    }
}
