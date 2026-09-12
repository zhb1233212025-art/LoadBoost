using System;
using System.Collections.Generic;
using LoadBoost.Core;
using Xunit;

namespace LoadBoost.Tests
{
    public sealed class ReportBuilderTests
    {
        private static LoadReportData Sample()
        {
            return new LoadReportData
            {
                Timestamp = new DateTime(2026, 7, 31, 12, 0, 0),
                TotalLoadSeconds = 100.0,
                Phases = new List<PhaseTiming>
                {
                    new PhaseTiming { Name = "GameDatabase", Seconds = 75.0 },
                    new PhaseTiming { Name = "PartLoader", Seconds = 15.0 }
                },
                DiskStats = new List<ModDiskStats>
                {
                    new ModDiskStats { ModName = "ModBig", TotalBytes = 2000000000, FileCount = 100 },
                    new ModDiskStats { ModName = "ModSmall", TotalBytes = 1000, FileCount = 2 }
                },
                LoadedAssets = new List<LoadedModAssets>
                {
                    new LoadedModAssets { ModName = "Squad", TextureCount = 500, ModelCount = 300, AudioCount = 10 }
                },
                Prewarm = new PrewarmStats { FilesRead = 42, BytesRead = 1610612736, FilesFailed = 1, Elapsed = TimeSpan.FromSeconds(9.5) }
            };
        }

        [Fact]
        public void Build_ContainsPhasesPercentAndUnattributed()
        {
            var text = ReportBuilder.Build(Sample());
            Assert.Contains("GameDatabase", text);
            Assert.Contains("75.0%", text);
            Assert.Contains("(未归因)", text);
            Assert.Contains("10.0%", text); // 100 - 75 - 15
        }

        [Fact]
        public void Build_DiskStatsKeepDescendingOrder()
        {
            var text = ReportBuilder.Build(Sample());
            Assert.True(text.IndexOf("ModBig", StringComparison.Ordinal) <
                        text.IndexOf("ModSmall", StringComparison.Ordinal));
        }

        [Fact]
        public void Build_ContainsPrewarmAndLoadedAssets()
        {
            var text = ReportBuilder.Build(Sample());
            Assert.Contains("1.5 GB", text);  // 预热字节数
            Assert.Contains("Squad", text);
        }

        [Fact]
        public void Build_ToleratesNullSections()
        {
            var data = Sample();
            data.DiskStats = null;
            data.LoadedAssets = null;
            data.Prewarm = null;
            var text = ReportBuilder.Build(data);
            Assert.Contains("(不可用)", text);
            Assert.Contains("(预热未启用)", text);
        }

        [Fact]
        public void FmtBytes_FormatsUnits()
        {
            Assert.Equal("1.5 GB", ReportBuilder.FmtBytes(1610612736));
            Assert.Equal("512 B", ReportBuilder.FmtBytes(512));
        }
    }
}
