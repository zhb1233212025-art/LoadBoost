# LoadBoost v0.1.1

KSP 1.12.x loading accelerator & stats — disk prewarm, phase timing, per-mod stats, and a first-launch welcome window.

KSP 1.12.x 加载加速与统计插件 —— 磁盘预热、阶段耗时、按 MOD 统计，并新增首启欢迎窗。

## Download / 下载

`LoadBoost-v0.1.1.zip` — extract into your KSP root so `GameData/LoadBoost/` sits under `GameData/`.
解压到 KSP 根目录，使 `GameData/LoadBoost/` 位于 `GameData/` 下。

**Requires / 依赖:** `000_Harmony`（多数整合包自带）。

## What's new / 更新内容

- **First-launch welcome window** — an IMGUI panel pops on first launch showing all settings, editable, with a Save button; bilingual zh/en following the game language; shown only once.
- **首启欢迎窗** —— 首次启动弹出 IMGUI 面板，展示并可编辑全部配置，带保存按钮；中英双语跟随游戏语言；仅弹一次。

## Features / 功能

- Disk prewarm (multi-threaded background pre-read of GameData into OS cache) / 磁盘预热（多线程后台预读）
- Loading phase timing (GameDatabase / PartLoader / unattributed) / 阶段耗时统计
- Per-mod disk usage & loaded-asset Top20 / 按 MOD 磁盘占用与已加载资产 Top20
- Scene-transition timing + F9 live perf panel / 场景切换计时 + F9 即时面板

## Configuration / 配置

`GameData/LoadBoost/LoadBoostSettings.txt`:

| Key | Default | 说明 |
|---|---|---|
| enablePrewarm | true | 磁盘预热开关 |
| prewarmThreads | 2 | 预热线程数 |
| reportEnabled | true | 生成加载报告 |
| verboseLog | false | 详细日志 |
| enableDiskScan | true | 磁盘占用扫描 |
| perfKey | F9 | 性能面板快捷键 |
| welcomeShown | false | 置 false 可让欢迎窗下次重弹 |

## Compatibility / 兼容

KSP 1.12.x。卸载 = 删除 `GameData/LoadBoost/`。
