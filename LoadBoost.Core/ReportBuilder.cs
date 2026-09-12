using System;
using System.Linq;
using System.Text;

namespace LoadBoost.Core
{
    public static class ReportBuilder
    {
        public static string Build(LoadReportData data)
        {
            var sb = new StringBuilder();
            sb.AppendLine("LoadBoost 加载报告");
            sb.AppendLine("生成时间: " + data.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine(string.Format("加载总耗时(Awake → 主菜单): {0:F1} s", data.TotalLoadSeconds));
            sb.AppendLine();

            sb.AppendLine("== 阶段耗时 ==");
            if (data.Phases == null || data.Phases.Count == 0)
            {
                sb.AppendLine("(未采集到阶段数据)");
            }
            else
            {
                double sum = 0;
                foreach (var p in data.Phases)
                {
                    sum += p.Seconds;
                    sb.AppendLine(FormatLine(p.Name, p.Seconds, data.TotalLoadSeconds));
                }
                sb.AppendLine(FormatLine("(未归因)", Math.Max(0, data.TotalLoadSeconds - sum), data.TotalLoadSeconds));
            }
            sb.AppendLine();

            sb.AppendLine("== 预热统计 ==");
            if (data.Prewarm == null)
            {
                sb.AppendLine("(预热未启用)");
            }
            else
            {
                sb.AppendLine(string.Format("文件 {0} 个,{1},耗时 {2:F1} s,失败 {3} 个",
                    data.Prewarm.FilesRead, FmtBytes(data.Prewarm.BytesRead),
                    data.Prewarm.Elapsed.TotalSeconds, data.Prewarm.FilesFailed));
            }
            sb.AppendLine();

            sb.AppendLine("== 按 MOD 磁盘占用 Top 20 ==");
            if (data.DiskStats == null)
            {
                sb.AppendLine("(不可用)");
            }
            else
            {
                foreach (var m in data.DiskStats.Take(20))
                    sb.AppendLine(string.Format("{0,-40} {1,10}  文件 {2,6}  纹理 {3}  模型 {4}  音频 {5}",
                        m.ModName, FmtBytes(m.TotalBytes), m.FileCount,
                        FmtBytes(m.TextureBytes), FmtBytes(m.ModelBytes), FmtBytes(m.AudioBytes)));
            }
            sb.AppendLine();

            sb.AppendLine("== 已加载运行时资产 Top 20(按纹理数)==");
            if (data.LoadedAssets == null)
            {
                sb.AppendLine("(不可用)");
            }
            else
            {
                foreach (var a in data.LoadedAssets.Take(20))
                    sb.AppendLine(string.Format("{0,-40} 纹理 {1,6}  模型 {2,6}  音频 {3,6}",
                        a.ModName, a.TextureCount, a.ModelCount, a.AudioCount));
            }
            return sb.ToString();
        }

        private static string FormatLine(string name, double seconds, double total)
        {
            double pct = total > 0 ? seconds / total * 100.0 : 0;
            return string.Format("{0,-20} {1,8:F1} s ({2:F1}%)", name, seconds, pct);
        }

        public static string FmtBytes(long bytes)
        {
            if (bytes >= 1L << 30) return (bytes / (double)(1L << 30)).ToString("F1") + " GB";
            if (bytes >= 1L << 20) return (bytes / (double)(1L << 20)).ToString("F1") + " MB";
            if (bytes >= 1L << 10) return (bytes / (double)(1L << 10)).ToString("F1") + " KB";
            return bytes + " B";
        }
    }
}
