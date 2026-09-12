using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LoadBoost
{
    /// <summary>
    /// 场景切换计时器:onGameSceneLoadRequested 起表,onLevelWasLoaded 停表,
    /// 追加写入 PluginData/scene-transitions.txt。跳过 LOADING→MAINMENU(启动加载,已有专门报告)。
    /// </summary>
    internal static class SceneTransitionTimer
    {
        private static readonly Stopwatch Watch = new Stopwatch();
        private static GameScenes _from;
        private static GameScenes _to;
        private static bool _timing;

        public static void Begin(GameScenes from, GameScenes to)
        {
            try
            {
                if (from == GameScenes.LOADING) { _timing = false; return; } // 启动加载不重复计
                _from = from;
                _to = to;
                _timing = true;
                Watch.Restart();
            }
            catch { _timing = false; }
        }

        public static void End(GameScenes scene)
        {
            if (!_timing) return;
            _timing = false;
            try
            {
                Watch.Stop();
                double seconds = Watch.Elapsed.TotalSeconds;
                string line = string.Format("{0:yyyy-MM-dd HH:mm:ss}  {1} → {2}  {3:F1} s",
                    DateTime.Now, _from, _to, seconds);
                var path = Path.Combine(LoadBoostPlugin.AsmDir, "PluginData", "scene-transitions.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                if (!File.Exists(path))
                    File.WriteAllText(path, "LoadBoost 场景切换计时\n", new UTF8Encoding(true));
                File.AppendAllText(path, line + "\n", new UTF8Encoding(false));
                Debug.Log("[LoadBoost] 场景切换: " + _from + " → " + _to + " 耗时 " + seconds.ToString("F1") + " s");
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 转场计时失败: " + e.Message);
            }
        }
    }
}
