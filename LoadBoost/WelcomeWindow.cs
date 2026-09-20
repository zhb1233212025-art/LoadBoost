using System;
using UnityEngine;

namespace LoadBoost
{
    internal sealed class WelcomeWindow : MonoBehaviour
    {
        private Settings _settings;
        private string _cfgPath;
        private Rect _rect;
        private bool _positioned;
        private bool _visible;

        private bool _prewarm, _report, _verbose, _diskScan;
        private string _threads, _perfKey;

        public void Show(Settings settings, string cfgPath)
        {
            _settings = settings;
            _cfgPath = cfgPath;
            _prewarm = settings.EnablePrewarm;
            _report = settings.ReportEnabled;
            _verbose = settings.VerboseLog;
            _diskScan = settings.EnableDiskScan;
            _threads = settings.PrewarmThreads.ToString();
            _perfKey = settings.PerfKey;
            _visible = true;
        }

        private void OnGUI()
        {
            if (!_visible) return;
            if (!_positioned)
            {
                _rect = new Rect((Screen.width - 440f) / 2f, (Screen.height - 400f) / 2f, 440f, 400f);
                _positioned = true;
            }
            GUI.skin = HighLogic.Skin;
            _rect = GUILayout.Window(
                GetInstanceID(), _rect, DrawWindow,
                WelcomeStrings.Title,
                GUILayout.Width(440f), GUILayout.Height(400f));
            _rect.x = Mathf.Clamp(_rect.x, 0f, Screen.width - _rect.width);
            _rect.y = Mathf.Clamp(_rect.y, 0f, Screen.height - _rect.height);
        }

        private void DrawWindow(int id)
        {
            GUILayout.BeginVertical();
            GUILayout.Label(WelcomeStrings.Intro);

            _prewarm = GUILayout.Toggle(_prewarm, WelcomeStrings.LblPrewarm);

            GUILayout.BeginHorizontal();
            GUILayout.Label(WelcomeStrings.LblThreads, GUILayout.Width(150f));
            _threads = GUILayout.TextField(_threads, GUILayout.Width(60f));
            GUILayout.EndHorizontal();

            _report = GUILayout.Toggle(_report, WelcomeStrings.LblReport);
            _verbose = GUILayout.Toggle(_verbose, WelcomeStrings.LblVerbose);
            _diskScan = GUILayout.Toggle(_diskScan, WelcomeStrings.LblDiskScan);

            GUILayout.BeginHorizontal();
            GUILayout.Label(WelcomeStrings.LblPerfKey, GUILayout.Width(150f));
            _perfKey = GUILayout.TextField(_perfKey, GUILayout.Width(80f));
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(WelcomeStrings.BtnSave, GUILayout.Width(120f)))
            {
                ApplyAndClose(true);
            }
            if (GUILayout.Button(WelcomeStrings.BtnClose, GUILayout.Width(120f)))
            {
                ApplyAndClose(false);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUI.DragWindow(new Rect(0f, 0f, _rect.width, 20f));
        }

        private void ApplyAndClose(bool save)
        {
            try
            {
                if (save && _settings != null)
                {
                    _settings.EnablePrewarm = _prewarm;
                    _settings.ReportEnabled = _report;
                    _settings.VerboseLog = _verbose;
                    _settings.EnableDiskScan = _diskScan;
                    int t;
                    if (int.TryParse(_threads, out t) && t >= 1) _settings.PrewarmThreads = t;
                    if (!string.IsNullOrWhiteSpace(_perfKey)) _settings.PerfKey = _perfKey.Trim();
                }
                if (_settings != null)
                {
                    _settings.WelcomeShown = true;
                    _settings.Save(_cfgPath);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 欢迎窗保存失败: " + e.Message);
            }
            _visible = false;
            Destroy(this);
        }
    }
}
