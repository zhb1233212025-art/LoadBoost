# Add first-launch welcome window / 新增首启欢迎窗

## Summary

Adds a first-launch welcome window to LoadBoost (KSP 1.12.x loading accelerator). On first launch at the main menu, an IMGUI panel shows all settings, editable, with a Save button; bilingual (zh/en) following the game language; shown only once via a `welcomeShown` flag.

为 LoadBoost 新增首启欢迎窗：首次启动到主菜单时弹出 IMGUI 面板，展示并可编辑全部配置，带保存按钮；中英双语跟随游戏语言；通过 `welcomeShown` 标记仅弹一次。

## Changes

- `LoadBoost/Settings.cs` — add `WelcomeShown` flag + `Save(cfgPath)`.
- `LoadBoost/WelcomeStrings.cs` — new bilingual (zh/en) string table, live `Localizer.CurrentLanguage`.
- `LoadBoost/WelcomeWindow.cs` — new IMGUI window; editable settings, Save/Close, lazy-centered, drag-clamped.
- `LoadBoost/LoadBoostPlugin.cs` — wire window to main-menu first launch.
- `LoadBoost/LoadBoost.csproj` — add `UnityEngine.IMGUIModule` reference.
- `README.md` — new bilingual project README incl. welcome window.

## Test plan

- [x] `dotnet build LoadBoost.csproj -c Release` — 0 errors.
- [x] `dotnet test LoadBoost.Tests` — 14/14 pass.
- [x] Real-game: first launch pops window; edit + Save writes config and sets `welcomeShown=True`; second launch no popup.
- [x] Language: window text follows game language (zh/en).
