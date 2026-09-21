# LoadBoost 实测：原版 vs 重度整合包加载对比

> 测试日期：2026-09-21。同一台机器、同一份 LoadBoost v0.1.2，仅 GameData 不同。
> 数据全部来自 LoadBoost 自身生成的加载报告（`GameData/LoadBoost/PluginData/load-report.txt`）。

## 测试环境

| 项 | 纯原版（D:\KSP-Stock） | 重度整合包（E 盘主安装） |
| --- | --- | --- |
| GameData 大小 | 2.2 GB | 12.0 GB |
| GameData mod 目录数 | 4（Squad / SquadExpansion / 000_Harmony / LoadBoost） | 70 |
| 磁盘上的文件数 | 3,759 | 18,336 |
| ModuleManager | 无 | 4.2.3 |

## 加载总耗时（Awake → 主菜单）

| | 纯原版 | 重度整合包 | 差异 |
| --- | --- | --- | --- |
| **总耗时** | **62.7 s** | **97.8 s** | +35.1 s（+56%） |

## 阶段耗时对比

| 阶段 | 纯原版 | 重度整合包 |
| --- | --- | --- |
| GameDatabase | 30.6 s（48.9%） | 38.1 s（38.9%） |
| PartLoader | 3.5 s（5.7%） | 38.1 s（39.0%） |
| ExpansionsLoader | 20.6 s（32.8%） | 0.5 s（0.5%） |
| 未归因 | 7.9 s（12.6%） | 21.1 s（21.6%） |

## 磁盘预热统计

| | 纯原版 | 重度整合包 |
| --- | --- | --- |
| 预读文件 | 3,759 个 | 18,336 个 |
| 预读字节 | 2.2 GB | 12.0 GB |
| 预热耗时 | 62.5 s（≈全程） | 97.8 s（≈全程） |
| 失败 | 0 | 0 |

## 重度整合包磁盘占用 Top 5（原版没有这些）

| MOD | 占用 |
| --- | --- |
| Bluedog_DB | 2.4 GB |
| Parallax_StockTerrainTextures | 1.9 GB |
| StockVolumetricClouds | 1.3 GB |
| Parallax_StockScatterTextures | 1.2 GB |
| Squad | 1.2 GB |

## 结论

1. **整合包比原版慢 56%（多 35 秒）**，主因是 PartLoader 从 3.5s 暴涨到 38.1s（+34.6s）——零件数量膨胀是加载变慢的头号来源。
2. **磁盘预热在原版+SSD 上收益极小**：原版 2.2 GB 预热线程跑满全程，但游戏本来读盘就够快，预热基本白抢 IO。这印证了 README 注意事项里"预热主要对 HDD / 超大整合包明显，SSD 提升很小"的说法。
3. **LoadBoost 的核心价值在重度整合包**：mod 越多、磁盘越慢，预热越有用，"按 MOD 找出谁占空间/谁占时间"也越有意义。纯原版用它主要是个加载计时/统计工具。

## 备注

- 两份报告均为 LoadBoost 自动生成，未做任何手工修改。
- 原版测试副本（`D:\KSP-Stock`）由主安装复制后删除第三方 mod 得到，未改动主安装。
