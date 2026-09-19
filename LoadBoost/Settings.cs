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
        public bool WelcomeShown = false;

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
                if (node.TryGetValue("welcomeShown", ref b)) s.WelcomeShown = b;
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 配置读取失败,使用默认值: " + e.Message);
            }
            return s;
        }

        public void Save(string cfgPath)
        {
            try
            {
                var node = new ConfigNode("LOADBOOST_SETTINGS");
                node.AddValue("enablePrewarm", EnablePrewarm);
                node.AddValue("prewarmThreads", PrewarmThreads);
                node.AddValue("reportEnabled", ReportEnabled);
                node.AddValue("verboseLog", VerboseLog);
                node.AddValue("enableDiskScan", EnableDiskScan);
                node.AddValue("perfKey", PerfKey);
                node.AddValue("welcomeShown", WelcomeShown);
                var root = new ConfigNode();
                root.AddNode(node);
                File.WriteAllText(cfgPath, root.ToString());
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 配置保存失败: " + e.Message);
            }
        }
    }
}
