# LoadBoost v0.1.1 模组本体逐行代码审查（2026-09-20）

两个独立审查流覆盖全部 21 个源文件（LoadBoost 13 个 + LoadBoost.Core 8 个），逐行通读。
审查后已实证核实关键疑点。

## 实证核实结果

- **`IsReady` 是方法不是属性**：用 Cecil 直接查 KSP 1.12.5 `Assembly-CSharp.dll`，`GameDatabase/PartLoader/FontLoader` 的 `IsReady` 均为普通方法（`method[IsReady]=True`，无 `get_IsReady` 属性）。故 `AccessTools.Method("IsReady")` 正确，**阶段计时功能正常**，子代理此条「高危」疑点不成立。
- **场景转场写文件无句柄泄漏**：复核 `File.AppendAllText`，每次 open/write/close，无泄漏。
- **合规**：全部文件未使用被禁的 `AppDomain.CurrentDomain.GetAssemblies()` + `Assembly.GetName()`；`PerfPatches` 正确使用 `AssemblyLoader.loadedAssemblies`。未发现后台线程调用 Unity API。

## 必须修复（高）

1. **`ReportGenerator.cs:25` 主线程阻塞等待磁盘扫描**
   `LoadBoostPlugin.DiskScanTask.Result` 在主菜单回调里无条件同步等待整盘扫描，与「不占主线程」的设计意图矛盾，最坏卡主菜单数十秒~数分钟。
   修法：仅 `IsCompleted` 时取结果，未就绪则在报告标注「扫描未完成」，或 `ContinueWith` 异步补写。

2. **`FileScanner.cs:16/30` 枚举无异常防护 + 不防 junction 循环**
   权限不足 / 扫描中目录被删 / 路径过长（>260）任一发生即全盘统计崩溃；`AllDirectories` 遇 junction/reparse 环会无限递归。
   修法：枚举过程包 try-catch 逐目录降级跳过，跳过 `FileAttributes.ReparsePoint` 目录。

3. **`PrewarmEngine.cs:33/44-57` `Finished` 启动窗口误判**
   worker 尚未 `Start()` 时 `IsAlive=false`，`Finished` 可能提前为 true，导致报告在预热刚启动时就生成近乎空的统计。
   修法：加 `_startComplete` volatile 标志或用 `CountdownEvent`。

## 建议改进（中）

4. `PerfReportWriter.cs:28` 写报告前未 `CreateDirectory("PluginData")`，特定路径下必失败 → 加一行建目录。
5. `WelcomeWindow.cs:42` 固定 400 高 + 无滚动，内容溢出时保存按钮不可达 → 加 ScrollView 或自适应高度。
6. `ReportBuilder.cs:54/68` Top20 不在本文件排序（`LoadedAssets` 标题称「按纹理数」却无 `OrderByDescending`），排序责任外置成脆弱契约 → 内置排序。
7. `PrewarmEngine.cs` `Start()` 并发防护与 `_started` 可见性；生产者枚举异常被静默吞掉（计入 `FilesFailed` 或记日志）。
8. `LoadedAssetCollector.cs:18` 无快照遍历 GameDatabase 列表 → 先 `.ToArray()`。
9. `PerfAggregator.cs:13-14` 普通集合 + 隐式「全主线程」假设 → 注释约束或换并发结构。
10. `SceneTransitionTimer.cs:36` 先置 `_timing=false` 再写文件，写失败丢本次数据。

## 建议改进（低）

- `Settings.cs:36` `prewarmThreads` 加载端无 `>=1` 校验
- `ReportGenerator.cs:26` AggregateException 只记 Message（应记 `GetBaseException()`）
- `PerfPatches.cs:46` 单方法 patch 失败完全静默（记 VerboseLog）
- `PerfProfiler.cs:21` 非法 PerfKey 静默回退 F9（记 LogWarning）
- `PhaseTracker.cs:25` Begin 幂等导致重载场景丢后续计时
- `PrewarmEngine.cs:104` worker 空转 `Sleep(10)` 可改 `BlockingCollection`
- `FileScanner.cs:33/43` 每文件 `FileInfo` 分配与 `ToLowerInvariant()` 字符串分配

## 总体评价

代码整体质量良好：线程同步基本功扎实（Interlocked/volatile/线程级兜底）、事件订阅退订配对正确、合规无雷。
3 个高危问题集中在「主线程阻塞」「扫描健壮性」「预热完成判定」，均为可修复的逻辑缺陷而非架构问题。
无崩溃性合规问题；`IsReady` 疑点经实证排除。
