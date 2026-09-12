using System;
using System.Collections.Generic;

namespace LoadBoost.Core
{
    public sealed class ModulePerfEntry
    {
        public string ModuleName = "";
        public string ModName = "";
        public double TotalMs;
        public long Calls;
    }

    public sealed class PerfSampleData
    {
        public DateTime Timestamp;
        public double DurationSeconds;
        public int FrameCount;
        public double AvgFrameMs;
        public double P95FrameMs;
        public List<ModulePerfEntry> Entries;
    }
}
