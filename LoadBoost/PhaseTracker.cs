using System;
using System.Collections.Generic;
using System.Diagnostics;
using LoadBoost.Core;

namespace LoadBoost
{
    internal static class PhaseTracker
    {
        private sealed class Entry
        {
            public string Name;
            public Stopwatch Watch;
            public bool Done;
        }

        public static DateTime StartUtc = DateTime.UtcNow;
        private static readonly object Sync = new object();
        private static readonly List<Entry> Entries = new List<Entry>();

        public static void Begin(string name)
        {
            lock (Sync)
            {
                if (Entries.Exists(e => e.Name == name)) return;
                Entries.Add(new Entry { Name = name, Watch = Stopwatch.StartNew() });
            }
        }

        public static void End(string name)
        {
            lock (Sync)
            {
                var e = Entries.Find(x => x.Name == name);
                if (e == null || e.Done) return;
                e.Watch.Stop();
                e.Done = true;
            }
        }

        public static List<PhaseTiming> Snapshot()
        {
            lock (Sync)
            {
                var list = new List<PhaseTiming>();
                foreach (var e in Entries)
                    list.Add(new PhaseTiming
                    {
                        Name = e.Done ? e.Name : e.Name + "(未结束)",
                        Seconds = e.Watch.Elapsed.TotalSeconds
                    });
                return list;
            }
        }
    }
}
