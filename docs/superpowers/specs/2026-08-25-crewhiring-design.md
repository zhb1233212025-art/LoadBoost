# CrewHiring —— MKS 宇航员招募界面剥离 + Kerbalism 职业联动 设计

日期：2026-08-25（2026-09-12 仓库重建时恢复）
状态：已实现并合并 master

## 背景与目标

把 USI Kolonization (MKS) 的宇航员中心（AC）自定义招募界面剥离为独立 MOD（不依赖 MKS/USITools），与 Kerbalism 兼容，并新增 MKS 职业与 Kerbalism 机制的联动（Medic 减少/治愈辐射之类）。保留 13 种职业与 MKS 的游戏内设置页。

### 关键事实（反编译 + 配置证据）

- AC 界面实现位于 KolonyTools.dll 的 `KolonyTools.AC`（源码参考副本在 `refs/mks/AC/`）：`EditACPrefab` 篡改 AC 的 UICanvasPrefab 藏掉原版申请人列表，`CustomAstronautComplexUISpawner` 计算屏幕 Rect 后挂 `CustomAstronautComplexUI`（IMGUI 招募窗口）。
- 费用/开关来自 `KolonyACOptions`（CustomParameterNode）：CoreCost=50000/KolonistCost=25000/MaxCost=500000 等。
- 10 个 MKS 专属职业 trait 定义在 `MKS/Kolonists.cfg`；其 USITools 专有 EFFECT（DrillSkill/MedicalSkill 等）脱离 USITools 后无意义，故剥离版改用原版效果类。
- Kerbalism 对 AC 的唯一触点是 tooltip postfix，不 hook 雇佣流程；`DB.Kerbal(name)` 懒建档——经自定义界面雇佣的 kerbal 会被 Kerbalism 正常建档，天然兼容。
- Kerbalism 联动挂钩点（反编译确认）：
  - `CrewSpecs.TraitMatches`（私有实例方法）：实验 crew_operate、Reliability 维修、Process/Harvester 工程师加成全部经此匹配。
  - `Process.Execute`：治疗类 process 的 cures 在执行时消费（cures 是 KERBALISM.Profile 静态单例对象的字段）。
  - `Rule.Execute`：逐 kerbal 累积 ruleData.problem；degeneration 为实例字段。
- 原版存在的效果类：AutopilotSkill、FullVesselControlSkill、GeeForceTolerance、EVAChuteSkill、RepairSkill、ConverterSkill、DrillSkill、FailureRepairSkill、VesselScienceReturn、PartScienceReturn、ScienceSkill。

## 架构

插件 `CrewHiring`（net481，`$(KspManaged)` 引用 + refs/harmony 0Harmony），部署 `GameData/CrewHiring/`（dll + Kolonists.cfg）。

1. **共存守卫** `ModGate.Active`：`AssemblyLoader.loadedAssemblies` 检测 KolonyTools，在场则全功能停用（禁 AppDomain.GetAssemblies）。
2. **AC 界面 1:1 移植**（`CrewHiring/AC/`）：命名空间改 CrewHiring.AC，`KolonyACOptions`→`HireOptions`，两处 ModGate 守卫，错误日志带 [CrewHiring] 前缀；性别/13职业/批量/勇气/愚蠢/Fearless/等级、MKS 费用公式逐行保持。
3. **职业 trait 配置** `GameData/CrewHiring/Kolonists.cfg`：10 个 EXPERIENCE_TRAIT（`:NEEDS[!KolonyTools]`），EFFECT 只用原版类：Miner→DrillSkill、Technician→ConverterSkill、Mechanic→RepairSkill+FailureRepairSkill、Biologist/Farmer→ScienceSkill、其余→GeeForceTolerance。
4. **Kerbalism 联动**（`KerbalismPatches`，运行期反射 patch，不编译期引用 Kerbalism）：
   - P1 职业视同（postfix `CrewSpecs.TraitMatches`）：Mechanic/Technician/Miner→Engineer；Biologist/Geologist/Farmer/Medic→Scientist；Scout→Pilot。单向，等级沿用自身。
   - P2 Medic 加速治疗（prefix `Process.Execute` 临时放大 cures，**finalizer 恢复**，异常路径也保证执行）：速率 ×(1+0.25×最高 Medic 等级)。
   - P3 Medic 辐射防护（prefix `Rule.Execute` 临时降低 radiation 规则 degeneration，**finalizer 恢复**）：每级 -6%。
   - 系数/开关在 `KerbalismSynergyOptions` 设置页（Section "CrewHiring"）。

## 验证（2026-08-26 通过）

- 21 个 xunit 单测（SynergyLogic）。
- 实机：KSP.log 确认初始化 + 三补丁挂载（视同=True 治疗=True 辐射=True）+ MM 解析 10 个 trait + 零 CrewHiring 异常。
- 终审修复：P2/P3 恢复改 finalizer（防止异常路径污染 Kerbalism 共享单例）、4 处日志补前缀、P1 不被 vdType/resType 缺失连坐。

## 明确不做（YAGNI）

- 救援契约整合、TRP-Hire 其它形态、UI 美化；不修改 Kerbalism/MKS 本体。

## 许可证

MKS 源码 GPL（界面借自 TRP-Hire 并获授权），移植保留出处注释。
