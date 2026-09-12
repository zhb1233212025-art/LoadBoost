using System;

namespace LoadBoost.Core
{
    public sealed class PrewarmStats
    {
        public long FilesRead;
        public long BytesRead;
        public long FilesFailed;
        public TimeSpan Elapsed;
    }
}
