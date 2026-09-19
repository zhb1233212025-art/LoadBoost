using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using LoadBoost.Core;
using UnityEngine;

namespace LoadBoost
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public sealed class LoadBoostPlugin : MonoBehaviour
    {
        internal static string AsmDir { get; private set; }
        internal static PrewarmEngine Prewarm { get; private set; }
        internal static Task<List<ModDiskStats>> DiskScanTask { get; private set; }

        private Settings _settings;
        private bool _reported;
        private string _cfgPath;
        private bool _welcomeShown;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            PhaseTracker.StartUtc = DateTime.UtcNow;
            AsmDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _cfgPath = Path.Combine(AsmDir, "LoadBoostSettings.txt");
            _settings = Settings.Load(_cfgPath);
            Settings.SetCurrent(_settings);
            Debug.Log("[LoadBoost] Awake,prewarm=" + _settings.EnablePrewarm + ",threads=" + _settings.PrewarmThreads);
            LoadingPatches.Apply();
            // GameDatabase.StartLoad 早于本 addon 实例化,Harmony 钩不到;
            // 改为从这里起算计时,用 OnGameDatabaseLoaded 事件收尾(与补丁幂等共存)
            PhaseTracker.Begin("GameDatabase");
            GameEvents.OnGameDatabaseLoaded.Add(OnGameDatabaseLoaded);
            if (_settings.EnablePrewarm) StartPrewarm();
            if (_settings.EnableDiskScan) StartDiskScan();
            GameEvents.onLevelWasLoaded.Add(OnLevelLoaded);
            GameEvents.onGameSceneLoadRequested.Add(OnSceneRequested);
        }

        private void StartPrewarm()
        {
            try
            {
                var gameData = Directory.GetParent(AsmDir).FullName;
                int threads = _settings.PrewarmThreads <= 0
                    ? Math.Min(2, Environment.ProcessorCount)
                    : _settings.PrewarmThreads;
                Prewarm = new PrewarmEngine(
                    Directory.EnumerateFiles(gameData, "*", SearchOption.AllDirectories), threads);
                Prewarm.Start();
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 预热启动失败: " + e);
            }
        }

        private void StartDiskScan()
        {
            try
            {
                var gameData = Directory.GetParent(AsmDir).FullName;
                // 后台扫描,主线程零等待;结果由 ReportGenerator 取用
                DiskScanTask = Task.Run(() => FileScanner.Scan(gameData));
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 磁盘扫描启动失败: " + e);
            }
        }

        private void OnGameDatabaseLoaded()
        {
            try { PhaseTracker.End("GameDatabase"); } catch { }
        }

        private void OnSceneRequested(GameScenes to)
        {
            try { SceneTransitionTimer.Begin(HighLogic.LoadedScene, to); } catch { }
        }

        private void OnLevelLoaded(GameScenes scene)
        {
            try { SceneTransitionTimer.End(scene); } catch { }
            if (_reported || scene != GameScenes.MAINMENU) return;
            _reported = true;
            try { if (Prewarm != null) Prewarm.Stop(); } catch { }
            if (_settings.ReportEnabled) ReportGenerator.WriteReport(_settings);
            if (!_welcomeShown && !_settings.WelcomeShown)
            {
                _welcomeShown = true;
                try
                {
                    var w = gameObject.AddComponent<WelcomeWindow>();
                    w.Show(_settings, _cfgPath);
                }
                catch (Exception e)
                {
                    Debug.LogError("[LoadBoost] 欢迎窗弹出失败: " + e.Message);
                }
            }
        }

        private void OnDestroy()
        {
            try { GameEvents.onLevelWasLoaded.Remove(OnLevelLoaded); } catch { }
            try { GameEvents.onGameSceneLoadRequested.Remove(OnSceneRequested); } catch { }
            try { GameEvents.OnGameDatabaseLoaded.Remove(OnGameDatabaseLoaded); } catch { }
            try { if (Prewarm != null) Prewarm.Stop(); } catch { }
        }
    }
}
