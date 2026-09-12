using System;
using UnityEngine;

namespace EngineLifecycle
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public sealed class EngineLifecyclePlugin : MonoBehaviour
    {
        private void Awake()
        {
            try
            {
                LifecyclePatches.Apply();
                GameEvents.onGameStatePostLoad.Add(OnGameLoaded);
                GameEvents.onGameStateSaved.Add(OnGameSaved);
            }
            catch (Exception e)
            {
                Debug.LogError("[EngineLifecycle] 初始化失败: " + e);
            }
        }

        private void OnGameLoaded(ConfigNode node)
        {
            try { LifecycleStore.Load(); } catch (Exception e) { Debug.LogError("[EngineLifecycle] " + e); }
        }

        private void OnGameSaved(Game game)
        {
            try { LifecycleStore.Save(); } catch (Exception e) { Debug.LogError("[EngineLifecycle] " + e); }
        }

        private void OnDestroy()
        {
            try { GameEvents.onGameStatePostLoad.Remove(OnGameLoaded); } catch { }
            try { GameEvents.onGameStateSaved.Remove(OnGameSaved); } catch { }
        }
    }
}
