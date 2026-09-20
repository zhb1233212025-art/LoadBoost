using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LoadBoost.Core;
using UnityEngine;

namespace LoadBoost
{
    internal static class PerfReportWriter
    {
        public static bool Write(float durationSeconds, int frames)
        {
            try
            {
                var frameMs = PerfAggregator.FrameMsSnapshot();
                var data = new PerfSampleData
                {
                    Timestamp = DateTime.Now,
                    DurationSeconds = durationSeconds,
                    FrameCount = frames,
                    AvgFrameMs = frameMs.Count > 0 ? frameMs.Average() : 0,
                    P95FrameMs = Percentile(frameMs, 0.95f),
                    Entries = PerfAggregator.SnapshotEntries()
                };
                var text = PerfReportBuilder.Build(data);
                var outDir = Path.Combine(LoadBoostPlugin.AsmDir, "PluginData");
                Directory.CreateDirectory(outDir);
                var outPath = Path.Combine(outDir, "perf-report.txt");
                File.WriteAllText(outPath, text, new UTF8Encoding(true));
                Debug.Log("[LoadBoost] 帧率报告: " + outPath);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 帧率报告生成失败: " + e);
                return false;
            }
        }

        private static float Percentile(List<float> values, float p)
        {
            if (values.Count == 0) return 0f;
            var sorted = new List<float>(values);
            sorted.Sort();
            int idx = (int)Math.Ceiling(sorted.Count * p) - 1;
            if (idx < 0) idx = 0;
            return sorted[idx];
        }
    }
}
