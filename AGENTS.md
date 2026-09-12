# AGENTS.md

## 会话授权原则（2026-08-17 用户指示）

- 用户在权限弹窗选择 "Yes, for this session" 后，同类工具调用在当前对话 session 内不再逐一请示（默认通过）。
- 该授权对整个会话有效，不限于某一条具体命令。
- 例外：致命或危险操作仍需明确确认——删除/覆盖用户数据、强制推送、git reset/rebase 等历史改写、终止非本任务启动的进程、修改游戏存档。
- 所有经此授权执行的操作必须在 git 提交记录或 `.superpowers/sdd/*/progress.md`（ledger）中可追溯。

## 环境事实（KSP1 插件开发，2026-09 实证）

- **主游玩安装：`E:\ksp\Kerbal Space Program`**（KSP 1.12.5，中文整合包，含 Kerbalism 3.42、Kopernicus、Principia、Parallax 等）。2026-09 用户重组磁盘后以此为准；旧路径 `E:\Kerbal Space Program` / `E:\Kerbal Space Program2` 已不存在。`E:\ksp\` 下还有其它独立安装（RP-1、principia 等），部署/验证一律以主安装为准。
- 本机 KSP 不写安装根 KSP.log——KSP.log 写到**进程 CWD**，插件日志同时也在 `Player.log`（%USERPROFILE%\AppData\LocalLow\Squad\Kerbal Space Program\Player.log；多个 KSP 安装共享同一 Player.log，最后运行的覆盖）。脚本化验证必须从游戏安装目录启动（KSP.log/glog 写安装目录属正常运行产物，勿从仓库根启动以免污染仓库）。
- 该整合包启动到主菜单约 2–4 分钟（KSPCF FastLoader 重建缓存时可达 7 分钟以上），脚本化轮询窗口至少 8 分钟；日志有假安静期（停笔 30s+），判定主菜单就绪要看 MainMenu 场景行，不能只看停笔。
- 本机 AppDomain 内有"毒"程序集：插件代码**禁止** `AppDomain.CurrentDomain.GetAssemblies()` + `Assembly.GetName()`（抛不可捕获的 ExecutionEngineException）。一律用 KSP 的 `AssemblyLoader.loadedAssemblies` 或程序集限定名 `Type.GetType`。
- 脚本化启动游戏的进程托管（2026-08-26 实证）：Bash 工具调用结束会清理其进程树，`./KSP_x64.exe &`、`nohup`、`Start-Process`、WMI `Win32_Process.Create` 拉起的游戏都会被回收。唯一可行方案是 `schtasks` 计划任务托管（任务计划程序是父进程），验证后记得 `schtasks /delete` 清理。
- Principia 原生 dll 加载时机（2026-08-17 实证）：其托管 Loader 用 **CWD 相对路径** `GameData/Principia/Windows/...` LoadLibrary——从临时目录启动游戏会导致原生插件**永不加载**。从安装目录正常启动时原生插件在游戏启动早期（主菜单前）即就绪。

## 仓库历史说明

- 2026-09-12：原仓库 `E:\MOD包\ksp2` 在磁盘整理中丢失，本仓库为按会话记录重建。CrewHiring 插件为完整重建；其余历史项目（PartFailures/EngineLifecycle/LoadBoost/NBodyMJ）源码未能恢复（游戏中已部署的 dll 仍在运行）。
