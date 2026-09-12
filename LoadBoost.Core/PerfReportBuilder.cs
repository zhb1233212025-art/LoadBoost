using System;
using System.Linq;
using System.Text;

namespace LoadBoost.Core
{
    public static class PerfReportBuilder
    {
        public static string Build(PerfSampleData data)
        {
            var sb = new StringBuilder();
            sb.AppendLine("LoadBoost 帧率体检报告");
            sb.AppendLine("生成时间: " + data.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
            double fps = data.AvgFrameMs > 0 ? 1000.0 / data.AvgFrameMs : 0;
            sb.AppendLine(string.Format("采样时长: {0:F1} s   帧数: {1}", data.DurationSeconds, data.FrameCount));
            sb.AppendLine(string.Format("平均帧时间: {0:F1} ms ({1:F1} FPS)   p95 帧时间: {2:F1} ms",
                data.AvgFrameMs, fps, data.P95FrameMs));
            sb.AppendLine();
            sb.AppendLine("== 模块帧耗时 Top 30 ==");
            if (data.Entries == null || data.Entries.Count == 0 || data.FrameCount <= 0)
            {
                sb.AppendLine("(无模块数据)");
            }
            else
            {
                double sumAll = 0, sumShown = 0;
                var ordered = data.Entries.OrderByDescending(e => e.TotalMs).ToList();
                foreach (var e in ordered) sumAll += e.TotalMs;
                foreach (var e in ordered.Take(30))
                {
                    sumShown += e.TotalMs;
                    sb.AppendLine(string.Format("{0,-45} {1,7:F2} ms/帧  {2,6:F1} 次/帧  [{3}]  计 {4:F0} ms",
                        e.ModuleName, e.TotalMs / data.FrameCount, (double)e.Calls / data.FrameCount,
                        e.ModName, e.TotalMs));
                }
                if (ordered.Count > 30)
                    sb.AppendLine(string.Format("(其余 {0} 个模块合计 {1:F2} ms/帧)",
                        ordered.Count - 30, (sumAll - sumShown) / data.FrameCount));
                sb.AppendLine(string.Format("(全部模块合计 {0:F2} ms/帧)", sumAll / data.FrameCount));
            }
            return sb.ToString();
        }
    }
}
