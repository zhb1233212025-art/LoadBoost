using static GameParameters;

namespace CrewHiring
{
    /// <summary>Kerbalism 职业联动设置（仅 Kerbalism 在场时有意义）。</summary>
    public class KerbalismSynergyOptions : CustomParameterNode
    {
        [CustomParameterUI("Profession Mapping", toolTip = "If enabled, MKS professions count as their stock counterparts in Kerbalism checks: Mechanic/Technician/Miner as Engineer, Biologist/Geologist/Farmer/Medic as Scientist, Scout as Pilot.", autoPersistance = true)]
        public bool ProfessionMapping = true;

        [CustomParameterUI("Medic Cure Bonus", toolTip = "If enabled, a Medic aboard speeds up sickbay-style cures (e.g. Kerbalism radiation detox).", autoPersistance = true)]
        public bool MedicCureBonus = true;

        [CustomFloatParameterUI("Cure Bonus Per Level", toolTip = "Cure rate bonus per Medic experience level.", autoPersistance = true, minValue = 0f, maxValue = 1f, stepCount = 20, displayFormat = "0.00")]
        public float CureBonusPerLevel = 0.25f;

        [CustomParameterUI("Medic Radiation Shielding", toolTip = "If enabled, a Medic aboard reduces radiation build-up for the crew.", autoPersistance = true)]
        public bool MedicRadiationShield = true;

        [CustomFloatParameterUI("Radiation Shield Per Level", toolTip = "Radiation build-up reduction per Medic experience level (0.06 = 6%).", autoPersistance = true, minValue = 0f, maxValue = 0.2f, stepCount = 20, displayFormat = "0.00")]
        public float RadiationShieldPerLevel = 0.06f;

        public static bool MappingEnabled => HighLogic.CurrentGame.Parameters.CustomParams<KerbalismSynergyOptions>().ProfessionMapping;
        public static bool CureBonusEnabled => HighLogic.CurrentGame.Parameters.CustomParams<KerbalismSynergyOptions>().MedicCureBonus;
        public static double GetCureBonusPerLevel => HighLogic.CurrentGame.Parameters.CustomParams<KerbalismSynergyOptions>().CureBonusPerLevel;
        public static bool RadiationShieldEnabled => HighLogic.CurrentGame.Parameters.CustomParams<KerbalismSynergyOptions>().MedicRadiationShield;
        public static double GetShieldPerLevel => HighLogic.CurrentGame.Parameters.CustomParams<KerbalismSynergyOptions>().RadiationShieldPerLevel;

        public override string Section => "CrewHiring";
        public override string DisplaySection => "CrewHiring";
        public override string Title => "Kerbalism Synergy";
        public override int SectionOrder => 1;
        public override GameMode GameMode => (GameMode)15;
        public override bool HasPresets => false;
    }
}
