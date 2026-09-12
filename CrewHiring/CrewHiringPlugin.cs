using System;
using UnityEngine;

namespace CrewHiring
{
    /// <summary>引导：应用 Kerbalism 联动补丁并打日志确认加载。招募界面由 AC/ 下的 KSPAddon 驱动。</summary>
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public sealed class CrewHiringPlugin : MonoBehaviour
    {
        private void Awake()
        {
            try
            {
                if (!ModGate.Active)
                {
                    Debug.Log("[CrewHiring] 检测到 MKS (KolonyTools)，本插件停用");
                    return;
                }
                KerbalismPatches.Apply();
                Debug.Log("[CrewHiring] 初始化完成 v1.0.0");
            }
            catch (Exception e)
            {
                Debug.LogError("[CrewHiring] 初始化失败: " + e);
            }
        }
    }
}
