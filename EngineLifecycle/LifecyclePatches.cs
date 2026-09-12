using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using KERBALISM.EngineFailures;
using ScrapYard.Modules;
using UnityEngine;

namespace EngineLifecycle
{
    internal static class LifecyclePatches
    {
        public static void Apply()
        {
            try
            {
                var harmony = new Harmony("ksp.enginelifecycle");

                var heuristics = AccessTools.TypeByName("KERBALISM.EngineFailures.EngineReliabilityHeuristics");
                var efType = typeof(EngineFailures);
                var apply = heuristics == null ? null : AccessTools.Method(heuristics, "Apply", new[] { efType });
                if (apply != null)
                    harmony.Patch(apply, postfix: new HarmonyMethod(typeof(LifecyclePatches), nameof(ApplyPostfix)));
                else
                    Debug.LogWarning("[EngineLifecycle] 未找到 EngineReliabilityHeuristics.Apply,复用/增益不生效");

                var brk = AccessTools.Method(efType, "Break");
                if (brk != null)
                    harmony.Patch(brk, postfix: new HarmonyMethod(typeof(LifecyclePatches), nameof(BreakPostfix)));
                else
                    Debug.LogWarning("[EngineLifecycle] 未找到 EngineFailures.Break,试车增益不生效");

                Debug.Log("[EngineLifecycle] 补丁完成: apply=" + (apply != null) + " break=" + (brk != null));
            }
            catch (Exception e)
            {
                Debug.LogError("[EngineLifecycle] 补丁应用失败: " + e);
            }
        }

        /// <summary>KEF 每次会话对每模块仅执行一次:先由 KEF 写入评级,我们再缩放。评级字段非持久化,天然幂等。</summary>
        public static void ApplyPostfix(object __0)
        {
            try
            {
                var module = __0 as EngineFailures;
                if (module == null || module.part == null) return;
                LifecycleSettings.EnsureLoaded();

                double reuse = ReuseFactor(module.part);
                double bonus = FamilyBonus(module);
                double factor = reuse * bonus;
                if (Math.Abs(factor - 1.0) < 1e-9 && Math.Abs(bonus - 1.0) < 1e-9) return;

                if (module.rated_operation_duration > 0)
                    module.rated_operation_duration *= factor;
                if (module.rated_ignitions > 0)
                    module.rated_ignitions = Math.Max(1, (int)Math.Round(module.rated_ignitions * factor));
                if (bonus > 1.0 && module.turnon_failure_probability > 0)
                    module.turnon_failure_probability /= bonus;

                Debug.Log(string.Format(
                    "[EngineLifecycle] {0}: 复用×{1:F2} 试车×{2:F2} → 时长 {3:F0}s / 点火 {4} 次 / 点火失败率 {5:F4}",
                    module.part.partInfo != null ? module.part.partInfo.name : module.part.name,
                    reuse, bonus, module.rated_operation_duration, module.rated_ignitions, module.turnon_failure_probability));
            }
            catch (Exception e)
            {
                Debug.LogError("[EngineLifecycle] 评级缩放失败: " + e);
            }
        }

        /// <summary>试车判定:Break 造成实际损伤(排除安全模式;碰撞不走 Break,天然排除)。</summary>
        public static void BreakPostfix(object __instance)
        {
            try
            {
                var module = __instance as EngineFailures;
                if (module == null) return;

                bool exploded = Traverse.Create(module).Field("explode").GetValue<bool>();
                if (!module.broken && !exploded) return;

                string family = ResolveFamily(module);
                LifecycleStore.RegisterTest(family);
                double bonus = LifecycleStore.BonusFor(family);
                string msg = string.Format("[EngineLifecycle] 试车数据: {0} 族第 {1} 次,可靠性增益 ×{2:F2}",
                    family, LifecycleStore.CountFor(family), bonus);
                Debug.Log(msg);
                try { ScreenMessages.PostScreenMessage(msg, 5f); } catch { }
            }
            catch (Exception e)
            {
                Debug.LogError("[EngineLifecycle] 试车登记失败: " + e);
            }
        }

        /// <summary>复用衰减:库存件且被回收过 → 0.9^TimesRecovered;新件 → 1。</summary>
        private static double ReuseFactor(Part part)
        {
            try
            {
                var tracker = part.FindModulesImplementing<ModuleSYPartTracker>().FirstOrDefault();
                if (tracker == null || !tracker.Inventoried || tracker.TimesRecovered <= 0) return 1.0;
                return Math.Pow(LifecycleSettings.DecayPerUse, tracker.TimesRecovered);
            }
            catch { return 1.0; }
        }

        private static double FamilyBonus(EngineFailures module)
        {
            return LifecycleStore.BonusFor(ResolveFamily(module));
        }

        /// <summary>族解析:显式 family 字段 → KEF 的 FindModuleFamily → Classify(首发动机)→ 部件名兜底。</summary>
        private static string ResolveFamily(EngineFailures module)
        {
            try
            {
                var f = module.engine_reliability_family;
                if (!string.IsNullOrEmpty(f) && !f.Equals("auto", StringComparison.OrdinalIgnoreCase)) return f;

                var heuristics = AccessTools.TypeByName("KERBALISM.EngineFailures.EngineReliabilityHeuristics");
                if (heuristics != null && module.part != null)
                {
                    var find = AccessTools.Method(heuristics, "FindModuleFamily", new[] { typeof(Part) });
                    if (find != null)
                    {
                        var byModule = find.Invoke(null, new object[] { module.part }) as string;
                        if (!string.IsNullOrEmpty(byModule)) return byModule;
                    }
                    var engine = module.part.FindModulesImplementing<ModuleEngines>().FirstOrDefault();
                    var classify = AccessTools.Method(heuristics, "Classify", new[] { typeof(ModuleEngines) });
                    if (engine != null && classify != null)
                    {
                        var byClass = classify.Invoke(null, new object[] { engine }) as string;
                        if (!string.IsNullOrEmpty(byClass)) return byClass;
                    }
                }
                if (module.part != null && module.part.partInfo != null) return module.part.partInfo.name;
            }
            catch { }
            return "unknown";
        }
    }
}
