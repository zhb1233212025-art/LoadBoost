using System;
using System.IO;
using UnityEngine;

namespace LoadBoost
{
    internal sealed class Settings
    {
        public bool EnablePrewarm = true;
        public int PrewarmThreads = 2;
        public bool ReportEnabled = true;
        public bool VerboseLog = false;
        public bool EnableDiskScan = true;
        public string PerfKey = "F9";

        internal static Settings Current { get; private set; }
        internal static void SetCurrent(Settings s) { Current = s; }

        public static Settings Load(string cfgPath)
        {
            var s = new Settings();
            try
            {
                if (!File.Exists(cfgPath)) return s;
                var parsed = ConfigNode.Parse(File.ReadAllText(cfgPath));
                if (parsed == null) return s;
                var node = parsed.GetNode("LOADBOOST_SETTINGS");
                if (node == null && parsed.name == "LOADBOOST_SETTINGS") node = parsed;
                if (node == null) return s;
                bool b = false; int i = 0;
                if (node.TryGetValue("enablePrewarm", ref b)) s.EnablePrewarm = b;
                if (node.TryGetValue("reportEnabled", ref b)) s.ReportEnabled = b;
                if (node.TryGetValue("verboseLog", ref b)) s.VerboseLog = b;
                if (node.TryGetValue("enableDiskScan", ref b)) s.EnableDiskScan = b;
                if (node.TryGetValue("prewarmThreads", ref i)) s.PrewarmThreads = i;
                string sv = null;
                if (node.TryGetValue("perfKey", ref sv) && !string.IsNullOrEmpty(sv)) s.PerfKey = sv;
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 配置读取失败,使用默认值: " + e.Message);
            }
            return s;
        }
    }
}
