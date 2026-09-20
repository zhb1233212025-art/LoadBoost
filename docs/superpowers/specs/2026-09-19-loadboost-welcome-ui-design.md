# LoadBoost 首启欢迎界面 设计文档

日期：2026-09-19
状态：待用户确认
关联发布：LoadBoost v0.1.1（GitHub Release）

## 1. 背景与目标

LoadBoost 是 KSP 1.12.x 的加载加速/统计插件。本次新增一个**首次启动欢迎界面**：

- 游戏**首次**启动 LoadBoost 时弹出一个欢迎窗口，之后不再弹（用标记记住"已弹过"）。
- 窗口内**展示并可编辑**现有配置参数，带"保存"按钮。
- 界面文字**跟随游戏语言**：中文(zh) / 英文(en)，其余语言回退英文。

UI 技术定为 **IMGUI（OnGUI）**：KSP 1.12 原生支持、改动小、无需打包资源。仓库内 CrewHiring 已有 OnGUI 先例（`CrewHiring/AC/CustomAstronautComplexUI.cs`）可参考。

## 2. 需求明细（用户已确认）

| 项 | 决定 |
|---|---|
| 触发时机 | 游戏首次启动（非手机；用户确认"手机"为笔误） |
| UI 技术 | IMGUI（OnGUI） |
| 参数呈现 | 可编辑 + 保存按钮 |
| 语言 | 中文 + 英文双语；其余语言回退英文 |

## 3. 功能设计

### 3.1 首启判定
- 在 `LoadBoostSettings.txt` 同目录（`GameData/LoadBoost/`）维护一个标记：新增配置项 `welcomeShown`（bool，默认 `false`）。
- 启动时读取：若 `welcomeShown != true` 则弹出欢迎窗；用户点保存或关闭后，把 `welcomeShown = true` 写回配置文件。
- 用户删除配置文件 / 重装 → 视为首次，重新弹出。

### 3.2 弹出时机
- KSP 主菜单场景加载完成后弹出（与现有报告生成同一场景钩子 `OnLevelLoaded(MAINMENU)`），避免在加载黑屏期弹窗看不见。

### 3.3 界面内容（可编辑参数）
展示并允许编辑以下项（与 `Settings.cs` 一一对应）：
- 预热开关 `enablePrewarm`（勾选框）
- 预热线程数 `prewarmThreads`（整数输入框）
- 生成报告 `reportEnabled`（勾选框）
- 详细日志 `verboseLog`（勾选框）
- 磁盘扫描 `enableDiskScan`（勾选框）
- 性能面板快捷键 `perfKey`（文本输入框）

底部按钮：
- **保存 / Save**：把当前值写回 `LoadBoostSettings.txt`，置 `welcomeShown=true`，关窗。
- **关闭 / Close**（右上角 X）：不保存改动，但仍置 `welcomeShown=true`（避免每次启动都弹），关窗。

### 3.4 语言识别与文案
- 读取 KSP 当前语言：`Localizer.CurrentLanguage`（KSP 内置 API）。`zh`/`zh-cn`/`zh-tw` → 中文界面；其余 → 英文。
- 文案表内置两份（中英），按当前语言取值；取不到的语言一律英文。
- 标题、各参数标签、按钮文字均走该文案表。

## 4. 代码结构

新增一个文件，避免膨胀现有类：

```
LoadBoost/
└── WelcomeWindow.cs   (新)  MonoBehaviour, OnGUI 绘制欢迎窗
```

- `WelcomeWindow`：负责窗口绘制、参数编辑态（临时副本）、语言文案、保存/关闭逻辑。
- `LoadBoostPlugin`：在 `OnLevelLoaded(MAINMENU)` 且 `!welcomeShown` 时实例化并显示 `WelcomeWindow`。
- `Settings`：新增 `welcomeShown` 字段的读写；新增 `Save(cfgPath)` 方法把当前配置写回文件（目前只有 Load，没有 Save，需要补）。

## 5. 依赖与兼容

- 仅使用 KSP/Unity 既有 API（IMGUI、Localizer），不新增外部依赖。
- 现有引用（Assembly-CSharp、UnityEngine.CoreModule、0Harmony）已够；OnGUI 在 `UnityEngine` 内，无需新引用。
- 兼容 KSP 1.12.x。

## 6. 编译与验证

- 本机已有 .NET 8 SDK，可编译 net48 目标（csproj 引用 `Microsoft.NETFramework.ReferenceAssemblies`）。
- 验证：
  1. Release 编译 0 错误。
  2. 部署到游戏，删除/置空 `welcomeShown` → 启动应弹窗。
  3. 改参数点保存 → 配置文件被更新、窗口关闭、再启动不再弹。
  4. 切换游戏语言中/英 → 界面文案对应切换。

## 7. 与发布的关系

- 该功能并入 v0.1.1 发布包；README 功能列表与配置段补充"首启欢迎窗"。
- 打包结构不变（仍 GameData/LoadBoost/ 两个 dll + LoadBoostSettings.txt）。

## 8. 范围外（YAGNI）

- 不做 uGUI 美化、不做更多语言、不做设置热重载（保存后重启生效即可）、不做手机端。
