using System;
using UnityEngine;

namespace LoadBoost
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public sealed class PerfProfiler : MonoBehaviour
    {
        private static bool _patchesApplied;
        private KeyCode _key = KeyCode.F9;
        private bool _sampling;
        private int _frames;
        private float _startTime;

        private void Awake()
        {
            var settings = Settings.Current;
            if (settings != null && !string.IsNullOrEmpty(settings.PerfKey))
            {
                KeyCode parsed;
                if (Enum.TryParse(settings.PerfKey, true, out parsed)) _key = parsed;
            }
            if (!_patchesApplied)
            {
                _patchesApplied = true;
                PerfPatches.Apply();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(_key)) Toggle();
            if (_sampling)
            {
                _frames++;
                PerfAggregator.AddFrame(Time.unscaledDeltaTime * 1000f);
            }
        }

        private void Toggle()
        {
            if (!_sampling)
            {
                PerfAggregator.Reset();
                PerfAggregator.Enabled = true;
                _sampling = true;
                _frames = 0;
                _startTime = Time.realtimeSinceStartup;
                PostMsg("[LoadBoost] 帧率采样中,再按 " + _key + " 结束并生成报告");
            }
            else
            {
                _sampling = false;
                PerfAggregator.Enabled = false;
                bool ok = PerfReportWriter.Write(Time.realtimeSinceStartup - _startTime, _frames);
                PostMsg(ok
                    ? "[LoadBoost] 帧率报告已写入 PluginData/perf-report.txt"
                    : "[LoadBoost] 帧率报告生成失败,详见 KSP.log");
            }
        }

        private static void PostMsg(string msg)
        {
            try { ScreenMessages.PostScreenMessage(msg, 4f); } catch { }
        }
    }
}
