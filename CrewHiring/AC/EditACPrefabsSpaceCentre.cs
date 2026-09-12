// Ported from MKS (KolonyTools.AC), GPL. Source: refs/mks/AC/EditACPrefabsSpaceCentre.cs
using UnityEngine;

namespace CrewHiring.AC
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    class EditACPrefabsSpaceCentre : MonoBehaviour
    {
        private void Awake()
        {
            if (!ModGate.Active) return;
            gameObject.AddComponent<EditACPrefab>();
        }
    }
}
