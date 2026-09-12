using System;
using System.Collections.Generic;
using System.Reflection;
using LoadBoost.Core;

namespace LoadBoost
{
    internal static class PerfAggregator
    {
        internal sealed class Stat { public double TotalMs; public long Calls; }

        public static bool Enabled;
        private static readonly Dictionary<Type, Stat> Stats = new Dictionary<Type, Stat>();
        private static readonly List<float> FrameMs = new List<float>();

        public static void Reset()
        {
            Stats.Clear();
            FrameMs.Clear();
        }

        public static void AddSample(Type moduleType, double ms)
        {
            Stat s;
            if (!Stats.TryGetValue(moduleType, out s)) { s = new Stat(); Stats[moduleType] = s; }
            s.TotalMs += ms;
            s.Calls++;
        }

        public static void AddFrame(float ms) { FrameMs.Add(ms); }

        public static List<float> FrameMsSnapshot() { return new List<float>(FrameMs); }

        public static List<ModulePerfEntry> SnapshotEntries()
        {
            var list = new List<ModulePerfEntry>();
            foreach (var kv in Stats)
                list.Add(new ModulePerfEntry
                {
                    ModuleName = kv.Key.FullName,
                    ModName = ModOfAssembly(kv.Key.Assembly),
                    TotalMs = kv.Value.TotalMs,
                    Calls = kv.Value.Calls
                });
            return list;
        }

        internal static string ModOfAssembly(Assembly asm)
        {
            try
            {
                var loc = asm.Location.Replace('\\', '/');
                var i = loc.IndexOf("/GameData/", StringComparison.OrdinalIgnoreCase);
                if (i >= 0)
                {
                    var rest = loc.Substring(i + "/GameData/".Length);
                    var j = rest.IndexOf('/');
                    return j > 0 ? rest.Substring(0, j) : rest;
                }
                if (loc.IndexOf("/KSP_x64_Data/", StringComparison.OrdinalIgnoreCase) >= 0) return "(原版)";
            }
            catch { }
            return "(其他)";
        }
    }
}
