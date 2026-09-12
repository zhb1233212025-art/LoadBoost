using System.Linq;

namespace CrewHiring
{
    /// <summary>共存守卫：检测到 MKS（KolonyTools）时本插件整体停用，避免双 UI 互改 AC prefab。</summary>
    public static class ModGate
    {
        private static bool? _active;

        public static bool Active
        {
            get
            {
                if (!_active.HasValue)
                {
                    // 禁止用 AppDomain.CurrentDomain.GetAssemblies()（本机有毒程序集，见 AGENTS.md）
                    _active = !AssemblyLoader.loadedAssemblies.Any(a => a.name == "KolonyTools");
                }
                return _active.Value;
            }
        }
    }
}
