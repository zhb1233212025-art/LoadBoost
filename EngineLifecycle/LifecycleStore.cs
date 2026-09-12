using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace EngineLifecycle
{
    /// <summary>
    /// 试车数据存储:每族成功试车次数。
    /// 不用 ScenarioModule(曾在加载时触发 stock NRE 并毒化存档流程),
    /// 改为自管文件:saves/&lt;当前存档&gt;/EngineLifecycle.cfg,随存档各自独立。
    /// </summary>
    internal static class LifecycleStore
    {
        private static readonly Dictionary<string, int> Counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public static int CountFor(string family)
        {
            int n;
            return family != null && Counts.TryGetValue(family, out n) ? n : 0;
        }

        public static double BonusFor(string family)
        {
            int n = CountFor(family);
            if (n <= 0) return 1.0;
            return Math.Min(LifecycleSettings.TestBonusCap, Math.Pow(LifecycleSettings.TestBonusPerTest, n));
        }

        public static void RegisterTest(string family)
        {
            if (string.IsNullOrEmpty(family)) return;
            Counts[family] = CountFor(family) + 1;
            Save(); // 立即落盘,崩游戏也不丢试车数据
        }

        public static void Load()
        {
            Counts.Clear();
            try
            {
                var path = StorePath();
                if (path == null || !File.Exists(path)) return;
                var root = ConfigNode.Parse(File.ReadAllText(path));
                var node = root != null && root.name == "ENGINE_LIFECYCLE" ? root : root == null ? null : root.GetNode("ENGINE_LIFECYCLE");
                if (node == null) return;
                foreach (ConfigNode.Value v in node.values)
                {
                    int n;
                    if (int.TryParse(v.value, out n)) Counts[v.name] = n;
                }
                Debug.Log("[EngineLifecycle] 试车数据已加载: " + Counts.Count + " 族 ← " + path);
            }
            catch (Exception e)
            {
                Debug.LogError("[EngineLifecycle] 试车数据加载失败: " + e);
            }
        }

        public static void Save()
        {
            try
            {
                var path = StorePath();
                if (path == null) return;
                var node = new ConfigNode("ENGINE_LIFECYCLE");
                foreach (var kv in Counts)
                    node.AddValue(kv.Key, kv.Value);
                node.Save(path);
            }
            catch (Exception e)
            {
                Debug.LogError("[EngineLifecycle] 试车数据保存失败: " + e);
            }
        }

        private static string StorePath()
        {
            try
            {
                // GameData/EngineLifecycle/EngineLifecycle.dll → 上两级 = KSP 根
                var root = Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                var saveFolder = HighLogic.SaveFolder; // 当前存档名(如 "生涯1");主菜单等无游戏状态时为默认
                if (string.IsNullOrEmpty(saveFolder)) return null;
                return Path.Combine(root, "saves", saveFolder, "EngineLifecycle.cfg");
            }
            catch { return null; }
        }
    }
}
