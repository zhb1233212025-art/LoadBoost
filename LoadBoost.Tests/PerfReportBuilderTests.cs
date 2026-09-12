using System;
using System.Collections.Generic;
using LoadBoost.Core;
using Xunit;

namespace LoadBoost.Tests
{
    public sealed class PerfReportBuilderTests
    {
        private static PerfSampleData Sample()
        {
            return new PerfSampleData
            {
                Timestamp = new DateTime(2026, 7, 31, 20, 0, 0),
                DurationSeconds = 100.0,
                FrameCount = 100,
                AvgFrameMs = 25.0,
                P95FrameMs = 40.0,
                Entries = new List<ModulePerfEntry>
                {
                    new ModulePerfEntry { ModuleName = "Kerbalism.Sim", ModName = "Kerbalism", TotalMs = 500, Calls = 200 },
                    new ModulePerfEntry { ModuleName = "FAR.Aero", ModName = "FerramAerospaceResearch", TotalMs = 100, Calls = 100 }
                }
            };
        }

        [Fact]
        public void Build_ComputesPerFrameAndFps()
        {
            var text = PerfReportBuilder.Build(Sample());
            Assert.Contains("40.0 FPS", text);          // 1000/25
            Assert.Contains("5.00 ms/帧", text);        // 500/100
            Assert.Contains("2.0 次/帧", text);         // 200/100
            Assert.Contains("Kerbalism.Sim", text);
        }

        [Fact]
        public void Build_SortsByTotalMsDescending()
        {
            var text = PerfReportBuilder.Build(Sample());
            Assert.True(text.IndexOf("Kerbalism.Sim", StringComparison.Ordinal) <
                        text.IndexOf("FAR.Aero", StringComparison.Ordinal));
        }

        [Fact]
        public void Build_ToleratesEmptyData()
        {
            var data = Sample();
            data.Entries = null;
            Assert.Contains("(无模块数据)", PerfReportBuilder.Build(data));
            data.Entries = new List<ModulePerfEntry>();
            Assert.Contains("(无模块数据)", PerfReportBuilder.Build(data));
            data.Entries.Add(new ModulePerfEntry { ModuleName = "X", ModName = "Y", TotalMs = 1 });
            data.FrameCount = 0;
            Assert.Contains("(无模块数据)", PerfReportBuilder.Build(data));
        }

        [Fact]
        public void Build_ShowsRemainderBeyondTop30()
        {
            var data = Sample();
            for (int i = 0; i < 40; i++)
                data.Entries.Add(new ModulePerfEntry { ModuleName = "Mod" + i, ModName = "M", TotalMs = i + 1 });
            var text = PerfReportBuilder.Build(data);
            Assert.Contains("其余", text);
            Assert.Contains("全部模块合计", text);
        }
    }
}
