using System;
using KSP.Localization;

namespace LoadBoost
{
    internal static class WelcomeStrings
    {
        internal static bool IsChinese
        {
            get
            {
                string lang = null;
                try { lang = Localizer.CurrentLanguage; } catch { }
                return !string.IsNullOrEmpty(lang)
                    && lang.StartsWith("zh", StringComparison.OrdinalIgnoreCase);
            }
        }

        internal static string Title => IsChinese ? "欢迎使用 LoadBoost" : "Welcome to LoadBoost";
        internal static string Intro => IsChinese
            ? "首次启动。以下为当前配置，可直接修改后保存。"
            : "First launch. Review and edit the settings below, then save.";
        internal static string LblPrewarm => IsChinese ? "磁盘预热" : "Disk prewarm";
        internal static string LblThreads => IsChinese ? "预热线程数" : "Prewarm threads";
        internal static string LblReport => IsChinese ? "生成加载报告" : "Generate load report";
        internal static string LblVerbose => IsChinese ? "详细日志" : "Verbose log";
        internal static string LblDiskScan => IsChinese ? "磁盘占用扫描" : "Disk usage scan";
        internal static string LblPerfKey => IsChinese ? "性能面板快捷键" : "Perf panel key";
        internal static string BtnSave => IsChinese ? "保存" : "Save";
        internal static string BtnClose => IsChinese ? "关闭" : "Close";
    }
}
