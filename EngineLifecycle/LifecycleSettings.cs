using System;
using UnityEngine;

namespace EngineLifecycle
{
    /// <summary>运行参数:GameData/EngineLifecycle/EngineLifecycle.cfg 的 ENGINE_LIFECYCLE_SETTINGS 节点,缺省用默认值。</summary>
    internal static class LifecycleSettings
    {
        public static double DecayPerUse = 0.9;      // 每被回收复用一次,额定点火/时长 ×0.9
        public static double TestBonusPerTest = 1.3; // 每次成功试车(非碰撞损坏),族增益 ×1.3
        public static double TestBonusCap = 3.0;     // 族增益上限

        private static bool _loaded;

        public static void EnsureLoaded()
        {
            if (_loaded) return;
            _loaded = true;
            try
            {
                var node = GameDatabase.Instance == null ? null : GameDatabase.Instance.GetConfigNode("ENGINE_LIFECYCLE_SETTINGS");
                if (node == null) return;
                double d = 0;
                if (node.TryGetValue("decayPerUse", ref d) && d > 0 && d <= 1.0) DecayPerUse = d;
                if (node.TryGetValue("testBonusPerTest", ref d) && d >= 1.0) TestBonusPerTest = d;
                if (node.TryGetValue("testBonusCap", ref d) && d >= 1.0) TestBonusCap = d;
            }
            catch (Exception e)
            {
                Debug.LogError("[EngineLifecycle] 参数读取失败,使用默认值: " + e.Message);
            }
        }
    }
}
