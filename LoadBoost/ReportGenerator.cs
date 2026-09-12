using System;
using System.IO;
using System.Text;
using LoadBoost.Core;
using UnityEngine;

namespace LoadBoost
{
    internal static class ReportGenerator
    {
        public static void WriteReport(Settings settings)
        {
            try
            {
                var data = new LoadReportData
                {
                    Timestamp = DateTime.Now,
                    TotalLoadSeconds = (DateTime.UtcNow - PhaseTracker.StartUtc).TotalSeconds,
                    Phases = PhaseTracker.Snapshot(),
                    DiskStats = null,
                    LoadedAssets = null,
                    Prewarm = LoadBoostPlugin.Prewarm == null ? null : LoadBoostPlugin.Prewarm.Stats
                };
                // 磁盘扫描在插件 Awake 时已后台启动(LoadBoostPlugin.DiskScanTask),此处只取结果,不占主线程
                try { data.DiskStats = LoadBoostPlugin.DiskScanTask == null ? null : LoadBoostPlugin.DiskScanTask.Result; }
                catch (Exception e) { Debug.LogError("[LoadBoost] 磁盘扫描失败: " + e.Message); }
                try { data.LoadedAssets = LoadedAssetCollector.Collect(); }
                catch (Exception e) { Debug.LogError("[LoadBoost] 资产采集失败: " + e.Message); }

                var text = ReportBuilder.Build(data);
                var outDir = Path.Combine(LoadBoostPlugin.AsmDir, "PluginData");
                Directory.CreateDirectory(outDir);
                var outPath = Path.Combine(outDir, "load-report.txt");
                File.WriteAllText(outPath, text, new UTF8Encoding(true));
                Debug.Log(string.Format("[LoadBoost] 加载总耗时 {0:F1} s,报告: {1}", data.TotalLoadSeconds, outPath));
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 报告生成失败: " + e);
            }
        }
    }
}
