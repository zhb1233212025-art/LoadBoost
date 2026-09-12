using System;
using System.Collections.Generic;

namespace CrewHiring
{
    /// <summary>Kerbalism 职业联动的纯数值/映射逻辑（不依赖 KSP 运行期，便于单测）。</summary>
    public static class SynergyLogic
    {
        /// <summary>职业视同表（单向）：键为原版职业，值为可充任该原版职业的 MKS 职业。</summary>
        private static readonly Dictionary<string, string[]> Aliases = new Dictionary<string, string[]>
        {
            { "Engineer", new[] { "Mechanic", "Technician", "Miner" } },
            { "Scientist", new[] { "Biologist", "Geologist", "Farmer", "Medic" } },
            { "Pilot", new[] { "Scout" } },
        };

        /// <summary>actualTrait 是否能充任 requiredTrait（仅判别名，不含同名；同名由 Kerbalism 原版逻辑处理）。</summary>
        public static bool IsAliasOf(string requiredTrait, string actualTrait)
        {
            if (string.IsNullOrEmpty(requiredTrait) || string.IsNullOrEmpty(actualTrait)) return false;
            string[] list;
            if (!Aliases.TryGetValue(requiredTrait, out list)) return false;
            for (int i = 0; i < list.Length; i++)
                if (list[i] == actualTrait) return true;
            return false;
        }

        /// <summary>Medic 治疗速率加成：1 + 每级系数 × 最高 Medic 等级。</summary>
        public static double CureFactor(int maxMedicLevel, double perLevel)
        {
            return 1.0 + perLevel * Math.Max(0, maxMedicLevel);
        }

        /// <summary>Medic 辐射积累减免系数：1 - 每级系数 × 等级，钳到 [0,1]。</summary>
        public static double RadiationFactor(int maxMedicLevel, double perLevel)
        {
            double f = 1.0 - perLevel * Math.Max(0, maxMedicLevel);
            if (f < 0.0) f = 0.0;
            if (f > 1.0) f = 1.0;
            return f;
        }
    }
}
