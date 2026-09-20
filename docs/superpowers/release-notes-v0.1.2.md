# LoadBoost v0.1.2

KSP 1.12.x loading accelerator & stats — disk prewarm, phase timing, per-mod stats, first-launch welcome window.

KSP 1.12.x 加载加速与统计插件 —— 磁盘预热、阶段耗时、按 MOD 统计、首启欢迎窗。

## Download / 下载

**⬇ [LoadBoost-v0.1.2.zip](https://github.com/zhb1233212025-art/LoadBoost/releases/download/v0.1.2/LoadBoost-v0.1.2.zip)**

Extract into your KSP root so `GameData/LoadBoost/` sits under `GameData/`.
解压到 KSP 根目录，使 `GameData/LoadBoost/` 位于 `GameData/` 下。

**Requires / 依赖:** `000_Harmony`（多数整合包自带）。

## What's new / 更新内容

Bug-fix release based on a full line-by-line code review.

- **Fix: main-thread stall** — the load report no longer blocks the main menu waiting for the disk scan; if the scan isn't finished yet, the report notes it instead of freezing.
- **Fix: disk-scan robustness** — per-directory error tolerance (permission / deleted / over-long paths no longer crash the whole scan); skips junction/reparse points to avoid recursion loops.
- **Fix: prewarm "finished" misdetection** — the engine could report "done" before workers even started; fixed with a start-complete flag.
- **Fix: perf report** — creates `PluginData/` if missing instead of failing.
- **Fix: Top-20 lists** — disk usage and loaded-asset Top 20 are now actually sorted (loaded-asset list claimed "by texture count" but wasn't sorted).
- **Fix: settings validation** — `prewarmThreads` now clamped to ≥ 1 on load.

基于逐行代码审查的修复版本。

- **修复：主线程卡顿** —— 加载报告不再阻塞主菜单等待磁盘扫描；扫描未完成时改为标注，不再冻结。
- **修复：磁盘扫描健壮性** —— 逐目录容错（权限/被删/路径过长不再让整盘扫描崩溃）；跳过 junction/重解析点防递归循环。
- **修复：预热"已完成"误判** —— 引擎可能在预热线程启动前就误报完成，已用启动完成标志修复。
- **修复：帧率报告** —— `PluginData/` 目录缺失时自动创建，不再失败。
- **修复：Top-20 列表** —— 磁盘占用与已加载资产 Top 20 现在真正排序（资产段此前声称"按纹理数"却未排序）。
- **修复：配置校验** —— `prewarmThreads` 加载时钳制为 ≥ 1。
