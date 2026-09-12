using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace LoadBoost
{
    internal static class LoadingPatches
    {
        public static void Apply()
        {
            try
            {
                var harmony = new Harmony("ksp.loadboost");
                PatchLoader(harmony, typeof(GameDatabase));
                PatchLoader(harmony, typeof(PartLoader));
                PatchLoader(harmony, typeof(FontLoader));
                PatchLoader(harmony, typeof(Expansions.ExpansionsLoader));
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 补丁应用失败: " + e);
            }
        }

        private static void PatchLoader(Harmony harmony, Type loaderType)
        {
            try
            {
                var start = AccessTools.Method(loaderType, "StartLoad");
                var ready = AccessTools.Method(loaderType, "IsReady");
                if (start != null)
                    harmony.Patch(start, prefix: new HarmonyMethod(typeof(LoadingPatches), nameof(StartPrefix)));
                if (ready != null)
                    harmony.Patch(ready, postfix: new HarmonyMethod(typeof(LoadingPatches), nameof(ReadyPostfix)));
                if (start == null || ready == null)
                    Debug.LogWarning("[LoadBoost] " + loaderType.Name + " 钩子不完整: start=" + (start != null) + " ready=" + (ready != null));
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 补丁 " + loaderType.Name + " 失败: " + e);
            }
        }

        public static void StartPrefix(MethodBase __originalMethod)
        {
            try { PhaseTracker.Begin(__originalMethod.DeclaringType.Name); }
            catch { }
        }

        public static void ReadyPostfix(MethodBase __originalMethod, bool __result)
        {
            try { if (__result) PhaseTracker.End(__originalMethod.DeclaringType.Name); }
            catch { }
        }
    }
}
