using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LoadBoost
{
    internal static class PerfPatches
    {
        private static readonly string[] HookNames = { "Update", "FixedUpdate", "LateUpdate", "OnUpdate", "OnFixedUpdate" };

        public static void Apply()
        {
            try
            {
                var harmony = new Harmony("ksp.loadboost.perf");
                int scanned = 0, patched = 0;
                // AGENTS.md 约束:禁用 AppDomain.GetAssemblies(),改用 KSP 的 AssemblyLoader.loadedAssemblies
                foreach (var la in AssemblyLoader.loadedAssemblies)
                {
                    var asm = la.assembly;
                    if (asm == null) continue;
                    Type[] types;
                    try { types = asm.GetTypes(); }
                    catch (ReflectionTypeLoadException e) { types = e.Types.Where(t => t != null).ToArray(); }
                    catch { continue; }
                    foreach (var t in types)
                    {
                        if (t == null || t == typeof(PartModule) || t.ContainsGenericParameters || !typeof(PartModule).IsAssignableFrom(t)) continue;
                        scanned++;
                        foreach (var hook in HookNames)
                        {
                            MethodInfo m = null;
                            try { m = AccessTools.Method(t, hook, Type.EmptyTypes); } catch { }
                            if (m == null || m.DeclaringType != t || m.IsAbstract) continue;
                            try
                            {
                                harmony.Patch(m,
                                    prefix: new HarmonyMethod(typeof(PerfPatches), nameof(Prefix)),
                                    postfix: new HarmonyMethod(typeof(PerfPatches), nameof(Postfix)));
                                patched++;
                            }
                            catch { }
                        }
                    }
                }
                // 非 PartModule 的重点目标(如 Kerbalism 中央循环):同样走 Enabled 门控,开销可忽略
                int extras = PatchExtra(harmony);
                Debug.Log("[LoadBoost] 帧率体检: 扫描 " + scanned + " 个模块类型,挂钩 " + patched + " 个方法,额外挂钩 " + extras + " 个");
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 帧率体检补丁应用失败: " + e);
            }
        }

        // 非 PartModule 的重点目标:Kerbalism 中央循环(ScenarioModule,主线程跑全部船只的后台演算)
        private static readonly string[,] ExtraHooks = new string[,]
        {
            { "KERBALISM.Kerbalism", "Update" },
            { "KERBALISM.Kerbalism", "FixedUpdate" },
        };

        private static int PatchExtra(Harmony harmony)
        {
            int count = 0;
            for (int i = 0; i < ExtraHooks.GetLength(0); i++)
            {
                try
                {
                    var t = AccessTools.TypeByName(ExtraHooks[i, 0]);
                    var m = t == null ? null : AccessTools.Method(t, ExtraHooks[i, 1], Type.EmptyTypes);
                    if (m == null)
                    {
                        Debug.LogWarning("[LoadBoost] 帧率体检: 额外挂钩未命中 " + ExtraHooks[i, 0] + "." + ExtraHooks[i, 1]);
                        continue;
                    }
                    harmony.Patch(m,
                        prefix: new HarmonyMethod(typeof(PerfPatches), nameof(Prefix)),
                        postfix: new HarmonyMethod(typeof(PerfPatches), nameof(Postfix)));
                    count++;
                }
                catch { }
            }
            return count;
        }

        public static void Prefix(out Stopwatch __state)
        {
            __state = PerfAggregator.Enabled ? Stopwatch.StartNew() : null;
        }

        public static void Postfix(object __instance, Stopwatch __state)
        {
            if (__state == null) return;
            try { PerfAggregator.AddSample(__instance.GetType(), __state.Elapsed.TotalMilliseconds); }
            catch { }
        }
    }
}
