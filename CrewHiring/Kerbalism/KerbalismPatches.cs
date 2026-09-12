using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace CrewHiring
{
    /// <summary>
    /// Kerbalism 职业联动补丁。运行期反射定位（AccessTools.TypeByName），
    /// 不编译期引用 Kerbalism；任一目标找不到只影响对应补丁，其余照常。
    /// P2/P3 的"prefix 临时改字段"恢复由 finalizer 保证执行（原方法或链上其它补丁抛异常时也不例外）。
    /// </summary>
    internal static class KerbalismPatches
    {
        private static bool _applied;

        public static void Apply()
        {
            if (_applied) return;
            _applied = true;
            try
            {
                if (!AssemblyLoader.loadedAssemblies.Any(a => a.name == "Kerbalism"))
                {
                    Debug.Log("[CrewHiring] 未检测到 Kerbalism，联动补丁跳过");
                    return;
                }
                var harmony = new Harmony("ksp.crewhiring");

                // P1 职业视同（不依赖 VesselData/VesselResources，先挂，避免被连坐）
                var crewSpecs = AccessTools.TypeByName("KERBALISM.CrewSpecs");
                var traitMatches = crewSpecs == null ? null : AccessTools.Method(crewSpecs, "TraitMatches", new[] { typeof(string) });
                if (traitMatches != null)
                    harmony.Patch(traitMatches, postfix: new HarmonyMethod(typeof(KerbalismPatches), nameof(TraitMatchesPostfix)));
                else
                    Debug.LogWarning("[CrewHiring] 未找到 CrewSpecs.TraitMatches，职业视同不生效");

                // P2/P3 需要 VesselData/VesselResources 参与方法签名
                var vdType = AccessTools.TypeByName("KERBALISM.VesselData");
                var resType = AccessTools.TypeByName("KERBALISM.VesselResources");
                MethodInfo processExec = null;
                MethodInfo ruleExec = null;
                if (vdType == null || resType == null)
                {
                    Debug.LogError("[CrewHiring] 未找到 KERBALISM.VesselData/VesselResources，Medic 治疗/辐射防护跳过");
                }
                else
                {
                    var execSig = new[] { typeof(Vessel), vdType, resType, typeof(double) };

                    // P2 Medic 加速治疗
                    var processType = AccessTools.TypeByName("KERBALISM.Process");
                    processExec = processType == null ? null : AccessTools.Method(processType, "Execute", execSig);
                    if (processExec != null)
                        harmony.Patch(processExec,
                            prefix: new HarmonyMethod(typeof(KerbalismPatches), nameof(ProcessExecutePrefix)),
                            finalizer: new HarmonyMethod(typeof(KerbalismPatches), nameof(ProcessExecuteFinalizer)));
                    else
                        Debug.LogWarning("[CrewHiring] 未找到 Process.Execute，Medic 治疗加成不生效");

                    // P3 Medic 辐射防护
                    var ruleType = AccessTools.TypeByName("KERBALISM.Rule");
                    ruleExec = ruleType == null ? null : AccessTools.Method(ruleType, "Execute", execSig);
                    if (ruleExec != null)
                        harmony.Patch(ruleExec,
                            prefix: new HarmonyMethod(typeof(KerbalismPatches), nameof(RuleExecutePrefix)),
                            finalizer: new HarmonyMethod(typeof(KerbalismPatches), nameof(RuleExecuteFinalizer)));
                    else
                        Debug.LogWarning("[CrewHiring] 未找到 Rule.Execute，Medic 辐射防护不生效");
                }

                Debug.Log(string.Format("[CrewHiring] Kerbalism 联动补丁: 视同={0} 治疗={1} 辐射={2}",
                    traitMatches != null, processExec != null, ruleExec != null));
            }
            catch (Exception e)
            {
                Debug.LogError("[CrewHiring] 联动补丁应用失败: " + e);
            }
        }

        /// <summary>P1: 原版要求未命中时，查 MKS 职业视同表（required 可能是逗号列表）。</summary>
        public static void TraitMatchesPostfix(object __instance, string crewTrait, ref bool __result)
        {
            try
            {
                if (__result || !KerbalismSynergyOptions.MappingEnabled) return;
                var required = Traverse.Create(__instance).Field("trait").GetValue<string>();
                if (string.IsNullOrEmpty(required)) return;
                foreach (var r in required.Split(','))
                {
                    if (SynergyLogic.IsAliasOf(r.Trim(), crewTrait))
                    {
                        __result = true;
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[CrewHiring] 职业视同判定失败: " + e);
            }
        }

        /// <summary>P2 prefix: 治疗类 process（cures 非空）且船上有 Medic 时，临时放大 cures 数值（finalizer 负责恢复）。</summary>
        public static void ProcessExecutePrefix(object __instance, Vessel v, out Dictionary<string, double> __state)
        {
            __state = null;
            try
            {
                if (!KerbalismSynergyOptions.CureBonusEnabled || v == null) return;
                var cures = Traverse.Create(__instance).Field("cures").GetValue<Dictionary<string, double>>();
                if (cures == null || cures.Count == 0) return;
                int maxLevel = MaxMedicLevel(v);
                if (maxLevel <= 0) return;
                double factor = SynergyLogic.CureFactor(maxLevel, KerbalismSynergyOptions.GetCureBonusPerLevel);
                if (factor <= 1.0) return;
                __state = new Dictionary<string, double>(cures);
                foreach (var key in cures.Keys.ToList())
                    cures[key] *= factor;
            }
            catch (Exception e)
            {
                __state = null;
                Debug.LogError("[CrewHiring] Medic 治疗加成 prefix 失败: " + e);
            }
        }

        /// <summary>P2 finalizer: 恢复 cures 原值（异常路径也保证执行），原异常语义不变。</summary>
        public static Exception ProcessExecuteFinalizer(object __instance, Dictionary<string, double> __state, Exception __exception)
        {
            try
            {
                if (__state != null)
                {
                    var cures = Traverse.Create(__instance).Field("cures").GetValue<Dictionary<string, double>>();
                    if (cures != null)
                    {
                        foreach (var kv in __state)
                            cures[kv.Key] = kv.Value;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[CrewHiring] Medic 治疗加成恢复失败: " + e);
            }
            return __exception;
        }

        /// <summary>P3 prefix: radiation 规则且船上有 Medic 时，临时降低 degeneration（finalizer 负责恢复）。</summary>
        public static void RuleExecutePrefix(object __instance, Vessel v, ref double __state)
        {
            __state = -1.0;
            try
            {
                if (!KerbalismSynergyOptions.RadiationShieldEnabled || v == null) return;
                var tr = Traverse.Create(__instance);
                if (tr.Field("name").GetValue<string>() != "radiation") return;
                int maxLevel = MaxMedicLevel(v);
                if (maxLevel <= 0) return;
                double factor = SynergyLogic.RadiationFactor(maxLevel, KerbalismSynergyOptions.GetShieldPerLevel);
                if (factor >= 1.0) return;
                var field = tr.Field("degeneration");
                double original = field.GetValue<double>();
                __state = original;
                field.SetValue(original * factor);
            }
            catch (Exception e)
            {
                __state = -1.0;
                Debug.LogError("[CrewHiring] Medic 辐射防护 prefix 失败: " + e);
            }
        }

        /// <summary>P3 finalizer: 恢复 degeneration 原值（异常路径也保证执行），原异常语义不变。</summary>
        public static Exception RuleExecuteFinalizer(object __instance, double __state, Exception __exception)
        {
            try
            {
                if (__state >= 0.0)
                    Traverse.Create(__instance).Field("degeneration").SetValue(__state);
            }
            catch (Exception e)
            {
                Debug.LogError("[CrewHiring] Medic 辐射防护恢复失败: " + e);
            }
            return __exception;
        }

        /// <summary>船上（含未加载船 proto crew）最高 Medic 等级；无 Medic 返回 0。</summary>
        private static int MaxMedicLevel(Vessel v)
        {
            int max = 0;
            try
            {
                foreach (var c in v.GetVesselCrew())
                    if (c.trait == "Medic" && c.experienceLevel > max)
                        max = c.experienceLevel;
            }
            catch { }
            return max;
        }
    }
}
