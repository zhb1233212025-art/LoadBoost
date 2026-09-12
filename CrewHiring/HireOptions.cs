using static GameParameters;

namespace CrewHiring
{
    /// <summary>
    /// 宇航员中心招募设置（复刻 MKS KolonyACOptions，GPL 来源见 refs/mks/AC/）。
    /// </summary>
    public class HireOptions : CustomParameterNode
    {
        [CustomParameterUI("Customized Kerbonauts", toolTip = "If enabled, allows the customization of new Kerbonauts.", autoPersistance = true)]
        public bool CustomKerbonauts = true;

        [CustomParameterUI("Enable Kolonist Hiring", toolTip = "If enabled, allows the hiring of MKS specific Kerbonauts.", autoPersistance = true)]
        public bool KolonistHiring = true;

        [CustomParameterUI("Alternate Core Kerbonaut Costs", toolTip = "If enabled, overrides the stock Kerbonaut cost calculations with a new base cost for pilots, engineers, and scientists.", autoPersistance = true)]
        public bool AlternateCoreCost = true;

        [CustomIntParameterUI("Core Kerbonaut Cost", toolTip = "Base cost for Engineers, Pilots, and Scientists", autoPersistance = true, minValue = 0, maxValue = 1000000, stepSize = 1000)]
        public int CoreCost = 50000;

        [CustomParameterUI("Alternate Kolonist Costs", toolTip = "If enabled, overrides the stock Kerbonaut cost calculations with a new base cost for Kolonists.", autoPersistance = true)]
        public bool AlternateKolonistCost = true;

        [CustomIntParameterUI("Kolonist Cost", toolTip = "Base cost for secondary professions", autoPersistance = true, minValue = 0, maxValue = 1000000, stepSize = 1000)]
        public int KolonistCost = 25000;

        [CustomParameterUI("Enable Hiring Cost Cap", toolTip = "If enabled, puts a hard cap on the cost of a new hire", autoPersistance = true)]
        public bool CostCap = true;

        [CustomIntParameterUI("Max Hire Cost", toolTip = "The maximum cost of any hire", autoPersistance = true, minValue = 0, maxValue = 1000000, stepSize = 1000)]
        public int MaxCost = 500000;

        public static bool CustomKerbonautsEnabled => HighLogic.CurrentGame.Parameters.CustomParams<HireOptions>().CustomKerbonauts;
        public static bool KolonistHiringEnabled => HighLogic.CurrentGame.Parameters.CustomParams<HireOptions>().KolonistHiring;
        public static bool AlternateCoreCostEnabled => HighLogic.CurrentGame.Parameters.CustomParams<HireOptions>().AlternateCoreCost;
        public static int GetCoreCost => HighLogic.CurrentGame.Parameters.CustomParams<HireOptions>().CoreCost;
        public static bool AlternateKolonistCostEnabled => HighLogic.CurrentGame.Parameters.CustomParams<HireOptions>().AlternateKolonistCost;
        public static int GetKolonistCost => HighLogic.CurrentGame.Parameters.CustomParams<HireOptions>().KolonistCost;
        public static bool CostCapEnabled => HighLogic.CurrentGame.Parameters.CustomParams<HireOptions>().CostCap;
        public static int GetMaxCost => HighLogic.CurrentGame.Parameters.CustomParams<HireOptions>().MaxCost;

        public override string Section => "CrewHiring";
        public override string DisplaySection => "CrewHiring";
        public override string Title => "Astronaut Complex";
        public override int SectionOrder => 0;
        public override GameMode GameMode => (GameMode)15;
        public override bool HasPresets => false;
    }
}
