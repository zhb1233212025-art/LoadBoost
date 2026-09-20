# LoadBoost

A loading accelerator and loading-statistics plugin for Kerbal Space Program 1.12.x — pre-reads GameData into the OS disk cache and shows you exactly where loading time goes.

**[⬇ Download latest release 下载最新版](https://github.com/zhb1233212025-art/LoadBoost/releases/latest)**

---

## English

### Features

- **Disk prewarm**: background multi-threaded pre-read of GameData files into the OS cache, speeding up game loading.
- **Phase timing**: GameDatabase / PartLoader / unattributed loading phases, reported to the second.
- **Per-MOD statistics**: disk usage Top 20 and loaded runtime assets Top 20.
- **Scene-transition timing** plus an **F9 live performance panel**.
- **First-launch welcome window**: on the first run, an editable settings panel pops up (IMGUI; bilingual zh/en, follows the game language) with a Save button. It is shown only once.

### Installation

1. Download `LoadBoost-vX.Y.Z.zip` from the [latest release](https://github.com/zhb1233212025-art/LoadBoost/releases/latest) (Assets section).
2. Extract the zip into your KSP root folder, so that `GameData/LoadBoost` sits directly under `GameData`.
3. Requires `000_Harmony` to be present in `GameData`.

### Configuration

Settings live in `GameData/LoadBoost/LoadBoostSettings.txt` (KSP config-node format). All of these can also be edited from the welcome window.

| Setting | Default | Description |
| --- | --- | --- |
| `enablePrewarm` | `true` | Enable background disk prewarm of GameData. |
| `prewarmThreads` | `2` | Number of prewarm worker threads. |
| `reportEnabled` | `true` | Write loading statistics reports. |
| `verboseLog` | `false` | Verbose logging. |
| `enableDiskScan` | `true` | Enable per-MOD disk-usage scanning. |
| `perfKey` | `F9` | Hotkey for the live performance panel. |
| `welcomeShown` | `false` | Whether the welcome window has been shown. Set it back to `False` to make the welcome window appear again on next launch. |

### Compatibility

- KSP 1.12.x (tested on 1.12.5)
- Depends on `000_Harmony` (Harmony2) — required.
- No ModuleManager required; does not modify Kopernicus/Kerbalism or any other mod — it only times and reports on them.

### First run & ModuleManager cache

- **The first launch (or right after installing/updating any mod) is slower.** That is ModuleManager rebuilding its patch cache — normal, and not caused by LoadBoost; LoadBoost just reports that time honestly.
- **After that, as long as you don't change mods (add/remove/update mods, or manually delete `GameData/ModuleManager.ConfigSHA` / the MM cache), ModuleManager hits its cache and startup returns to normal speed.**
- If **every** launch is slow, some mod is usually rewriting a cfg each launch and invalidating the MM cache — check the report's "unattributed" section and the ModuleManager log to find it.

### Notes

- **Disk prewarm helps most on HDD / heavily-modded installs.** On NVMe/SATA SSDs the gain is small; you can disable it via `enablePrewarm` in the welcome window or settings file.
- Prewarm reads the whole GameData in the background during loading and may compete for disk I/O; on very low-end machines set `prewarmThreads` to `1`.
- The first-launch welcome window is shown only once. To reopen it, set `welcomeShown` back to `False` in `LoadBoostSettings.txt`.
- The **F9 live performance panel** works only in flight scenes.
- The plugin is **read-only**: it only reads GameData for prewarm/statistics and never modifies any game files.

### Uninstall

Delete the `GameData/LoadBoost` folder.

### Feedback

Report issues on GitHub: https://github.com/zhb1233212025-art/LoadBoost

---

## 中文

适用于 Kerbal Space Program 1.12.x 的加载加速与加载统计插件——将 GameData 预读进系统磁盘缓存，并告诉你加载时间究竟花在了哪里。

### 功能

- **磁盘预热**：后台多线程预读 GameData 文件进系统缓存，加快游戏加载。
- **阶段计时**：GameDatabase / PartLoader / 未归因阶段，精确到秒。
- **按 MOD 统计**：磁盘占用 Top 20、已加载运行时资源 Top 20。
- **场景切换计时**，以及 **F9 实时性能面板**。
- **首启欢迎窗**：首次启动弹出可编辑配置面板（IMGUI，中英双语，跟随游戏语言），带保存按钮，仅显示一次。

### 安装

1. 从 [最新发布](https://github.com/zhb1233212025-art/LoadBoost/releases/latest) 的 Assets 下载 `LoadBoost-vX.Y.Z.zip`。
2. 将 zip 解压到 KSP 根目录，使 `GameData/LoadBoost` 直接位于 `GameData` 之下。
3. 需要 `GameData` 中已存在 `000_Harmony`。

### 配置

设置文件位于 `GameData/LoadBoost/LoadBoostSettings.txt`（KSP config-node 格式），以上各项也可在欢迎窗中直接编辑。

| 设置项 | 默认值 | 说明 |
| --- | --- | --- |
| `enablePrewarm` | `true` | 是否启用 GameData 磁盘预热。 |
| `prewarmThreads` | `2` | 预热工作线程数。 |
| `reportEnabled` | `true` | 是否输出加载统计报告。 |
| `verboseLog` | `false` | 详细日志。 |
| `enableDiskScan` | `true` | 是否启用按 MOD 磁盘占用扫描。 |
| `perfKey` | `F9` | 实时性能面板热键。 |
| `welcomeShown` | `false` | 欢迎窗是否已显示。将其改回 `False` 可让欢迎窗在下次启动时重新弹出。 |

### 兼容性

- KSP 1.12.x（已在 1.12.5 实测）
- 依赖 `000_Harmony`（Harmony2），**必需**。
- 无需 ModuleManager；不修改 Kopernicus/Kerbalism 等任何 mod 的行为，仅对它们计时与统计。

### 首启与 ModuleManager 缓存

- **首次启动（或刚装/更新 mod 后）会比较慢**：这是 ModuleManager 在**重建补丁缓存**，属正常现象，与 LoadBoost 无关，LoadBoost 只是如实显示这段耗时。
- **之后只要你不改动 mod（不增删/更新 mod、不手动删 `GameData/ModuleManager.ConfigSHA` 或 MM 缓存），ModuleManager 会命中缓存，启动就恢复正常速度**。
- 如果**每次启动都慢**，通常是某个 mod 每次启动改写 cfg 导致 MM 缓存失效——可看报告里的「未归因」和 MM 日志定位。

### 注意事项

- **磁盘预热主要对机械硬盘 (HDD) 或超大整合包效果明显**；NVMe/SATA SSD 上提升很小，可在欢迎窗或配置文件里关闭 `enablePrewarm`。
- 预热会在加载期间后台读取整个 GameData，可能争抢磁盘 IO；极低配机器可把 `prewarmThreads` 调为 `1`。
- 首启欢迎窗只弹一次；想再次打开，把 `LoadBoostSettings.txt` 里 `welcomeShown` 改回 `False`。
- **F9 实时性能面板**仅在飞行场景生效。
- 本插件为**只读**：只读取 GameData 做预热与统计，不修改任何游戏文件。

### 卸载

删除 `GameData/LoadBoost` 文件夹即可。

### 反馈

请在 GitHub 提交 issue：https://github.com/zhb1233212025-art/LoadBoost
