using System;
using System.Collections.Generic;

namespace LoadBoost.Core
{
    public sealed class PhaseTiming
    {
        public string Name = "";
        public double Seconds;
    }

    public sealed class LoadedModAssets
    {
        public string ModName = "";
        public int TextureCount;
        public int ModelCount;
        public int AudioCount;
    }

    public sealed class LoadReportData
    {
        public DateTime Timestamp;
        public double TotalLoadSeconds;
        public List<PhaseTiming> Phases;
        public List<ModDiskStats> DiskStats;
        public List<LoadedModAssets> LoadedAssets;
        public PrewarmStats Prewarm;
    }
}
