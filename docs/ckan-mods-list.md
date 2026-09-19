# F:\ckan 模组清单

- 来源：F:\ckan 目录（CKAN 下载缓存），共 **1063 个文件**（约 58 GB），去重后约 **850 个不同模组**。
- 同一模组存在多个历史版本时合并为一行，并标注缓存中的版本数。
- 少数冷门模组无法确认确切功能，标注 `(?)` 表示按名称/常见用途推测。
- 注意：其中混有若干 **KSP2** 模组（SpaceWarp 系），已单独归类。

---

## 一、底层框架 / 依赖库

| 模组 | 作用 |
|---|---|
| ModuleManager | 几乎所有 KSP1 MOD 的基石，负责在加载时应用 MM 补丁 |
| Harmony2 | .NET 运行时方法补丁库，大量插件依赖 |
| ClickThroughBlocker | 阻止鼠标点击穿透 UI 窗口，大量插件依赖 |
| ToolbarController | 统一各 MOD 工具栏按钮 |
| Toolbar (blizzy) | 经典工具栏框架 |
| KSPCommunityFixes [4版本] | 大量官方 bug 修复与性能/画质改进 |
| CommunityFixes [2版本] | 社区 bug 修复集合 (?) |
| KSPCommunityPartModules [2版本] | 社区通用部件模块库 |
| KSPBurst / KSPBurst-Lite [各3版本] | 用 Burst 编译器加速游戏代码 |
| BurstPQS [2版本] | 用 Burst 加速地形(PQS)生成 |
| KSPTextureLoader [10版本] | 贴图加载优化/异步化 |
| Kopernicus [10版本] | 自定义星球/星系框架，所有星球包依赖 |
| KopernicusExpansionContinueder | Kopernicus 扩展功能（足迹等） |
| SigmaDimensions [2版本] | 星系缩放工具（2.5x/10x 等） |
| ModularFlightIntegrator | 替换飞行物理积分器（Kopernicus 等依赖） |
| KSP-Recall [2版本] | 修复部件资源被错误回收的引擎 bug |
| SerializationFix | 修复部件模块序列化 bug |
| KSP-PartVolume | 部件体积计算（货舱类 MOD 依赖） |
| FilterExtensions | 部件分类筛选框架 |
| CommunityCategoryKit | 自定义部件分类支持 |
| CommunityTraitIcons | 乘员职业/特性图标支持 |
| CommunityResourcePack / CommunityResources [各2版本] | 通用资源定义（各类资源 MOD 依赖） |
| MagiCore | 数学/通用工具库 |
| SpaceTuxLibrary | 依赖库（FinalFrontier 等） |
| AT-Utils | 部件工具库（GroundConstruction 等依赖） |
| JSIPartUtilities | IVA/RPM 类 MOD 依赖库 |
| ToadicusTools / ToadicusToolsContinued | 依赖库（EVAManager 等） |
| Benjee10-SharedAssets [2版本] | Benjee10 系部件包共享素材 |
| FireflyAPI | Firefly 再入特效的 API |
| WindAPI | 风力 API（KerbalWind 依赖） |
| Shabby / Shaddy | 着色器/材质辅助库 (?) |
| Harmony 系之外的补丁管理：PatchManager [3版本] | 管理/开关 MM 补丁 |
| PhysicsRangeExtender [2版本] | 扩大载具物理加载距离（BDArmory 依赖） |
| Inputbinder | 键位绑定库 |
| KSPSecondaryMotion | 部件次级物理摆动效果 |
| DepthMask | 深度遮罩（IVA 窗口/透明座舱依赖） |
| VertexColorMapEmissive [2版本] / VertexHeightOblateAdvanced / VertexMitchellNetravaliHeightMap | Kopernicus 星球材质/地形扩展 |
| LayeredAnimations | 分层动画支持库 |
| AnimationInitialization | 动画初始化辅助 (?) |
| StagedAnimation | 分级动画模块 |
| ModuleAnimateGenericEffects | 动画触发特效模块 |
| B9AnimationModules / BDAnimationModules | 通用动画模块库 |
| B9PartSwitch | 部件变体/油箱切换模块，大量部件包依赖 |
| InterstellarFuelSwitch-Core | 燃料类型切换模块 |
| InterstellarRedistributable | KSPIE 等依赖的运行库 |
| ConfigurableContainers-Core | 可配置容器核心模块 |
| KSP-AVC / ZeroMiniAVC | MOD 版本检查 |
| KerbalChangelog | 游戏内 MOD 更新日志 |
| TextAssetDumper / DebugTools / UnityExplorer | 调试/逆向工具 |
| LanguageChanger | 游戏语言切换 |
| KSPCasherCont | 缓存工具 (?) |
| NodeManager [2版本] | 节点管理工具 (?) |

## 二、画面 / 视觉 / 音效

| 模组 | 作用 |
|---|---|
| Scatterer + Scatterer-config + ScattererOPM | 大气散射、海面、日食等核心画质增强 |
| EnvironmentalVisualEnhancements (EVE) [2版本] + EVE-HR | 云层/极光/城市灯光 |
| Parallax + Parallax-StockTextures + Parallax-StockScatterTextures | 地表细分+散布物（石头/树）+碰撞 |
| ParallaxContinued [3版本] 及 Planet/Terrain/Scatter-Textures、KTL、Lifeless-Eve-Patch | Parallax 的续作版及配套贴图 |
| OuterParallax / OuterParallax-MPE [各2版本] | OPM/MPE 星球的 Parallax 配置 |
| QuackPack-ParallaxContinued | QuackPack 星球的 Parallax 配置 |
| CommunityTerrainTexturePack | Parallax 通用地形贴图 |
| AstronomersVisualPack (AVP) [2版本] + AVP-4k/8kTextures | 综合美化（EVE+Scatterer 配置） |
| Spectra | 综合视觉美化包 |
| PhotoRealisticVisualEnhancement (PRVE) | RSS 写实美化 |
| RSSVE-HR / RSSVE-LR | RSS 视觉增强（高/低配） |
| DistantObject [3版本] + DistantObject-RealSolarSystem | 远处天体/飞船真实渲染 |
| PlanetShine-Config-Default | 行星反照光照 |
| CelestialGlow | 天体辉光 |
| KabramsSunFlaresPack | 太阳耀斑贴图包 |
| PoodsCalmNebulaSkybox | 星云天空盒 |
| BetterKerbol | 太阳大气美化 |
| TechosStockPlanetRevamp [2版本] | 原版星球贴图重制 |
| TextureReplacer / TextureReplacerReplaced | 纹理/反射/宇航服替换框架 |
| WindowShineTR | 舱窗反光 |
| DiverseKerbalHeads | 小绿人头部多样化 |
| KerbalizedSuits / SpaceXSuits / Benjee10Suits / KHWearableKISProps | 宇航服外观 |
| ReflectiveVisors | 头盔面罩反射 |
| Waterfall [2版本] + StockWaterfallEffects + RestockWaterfallExpansion | 引擎尾焰特效框架及配置 |
| RealPlume + RealPlume-StockConfigs | 写实尾焰 |
| SmokeScreen | 烟雾/粒子特效框架 |
| Firefly [2版本] | 重写再入烧蚀火焰特效 |
| ReentryParticleEffect | 再入粒子增强 |
| Deferred [3版本] | 延迟渲染，让灯光真正照亮船体 |
| TUFX | 画面后处理（HDR、AO 等） |
| CinematicShaders [2版本] | 电影级着色器 (?) |
| Singularity [2版本] | 黑洞引力透镜着色器 |
| TexturesUnlimited [2版本] / Resurfaced | PBR 材质管线 |
| TURD-MakingHistory | TexturesUnlimited 的部件重着色配置 |
| EngineLighting / EngineLightRelit | 引擎火焰照亮周围 |
| ShineFix [2版本] | 光照修复 |
| OohShiny | 部件高光 (?) |
| Reflektor | 实时反射 (?) |
| VaporCones / VolumetricVaporCones | 跨音速激波锥特效 |
| VaporVent | 蒸汽排放特效 |
| DestructionEffects | 爆炸/坠毁特效 |
| CollisionFXReUpdated | 碰撞火花特效 |
| KerbalFX-Core/AeroFX/BlastFX/ImpactPuffs/RoverDust | 各类粒子特效集合 |
| KerbalWind | 旗子/部件随风摆动 |
| WaterSounds | 水花音效 |
| Chatterer + ChattererExtended | 无线电通话背景音 |
| KerbalPublicRadioServiceKPRS | 游戏内电台 |
| DockingPortSoundFX | 对接音效 |
| RocketSoundEnhancement [2版本] + Config-Default [2版本] | 火箭引擎音效系统 |
| ShipEffectsContinued | 船体音效/特效 |
| AtmosphericBeatsKSRSSconfigforEVEVolumetrics | EVE 体积云配套氛围包 (?) |
| NightShift | 夜间灯光增强 |
| SciFiVisualEnhancements | 科幻风格美化 (?) |
| HideOrbits | 隐藏/美化轨道线 |
| Commlines [2版本] | 通信链路连线可视化 |
| HUDReplacer [4版本] + ZTheme [2版本] | HUD/导航球替换及主题 |
| DarkMode | 深色 UI 主题 |
| LoadingScreenManager / LoadingTipsPlus / ROLoadingImages | 加载画面/提示 |
| SkipSplashScreen | 跳过启动 logo |
| StockScatterColliderEnablerPatch | 给原版散布物加碰撞 |
| InfiniteDiscoveriesEffects | 程序生成星球的特效 (?) |
| GrannusExpansionPack (GEP) 配套视觉见星球包区 | — |

## 三、飞行辅助 / 自动驾驶 / 信息显示

| 模组 | 作用 |
|---|---|
| MechJeb2 [5版本] + MechJeb2-dev [4版本] | 自动驾驶、发射/交会/着陆全辅助 |
| MechJebForAll | 让所有飞船自带 MechJeb 功能 |
| kOS [2版本] + kOS-Astrogator / kOS-KerbalEngineer / kOS-MechJeb2 / kOSPropMonitor | 脚本化飞控（kerboscript）及联动 |
| KontrolSystem2 | 新一代脚本飞控（to2 语言） |
| kRPC2 | 用 Python 等外部语言控制游戏 |
| KerbalEngineerRedux | Δv、推重比等数据显示 |
| Astrogator | 转移窗口/弹射角计算 |
| TransferWindowPlanner [+Fork] | 行星转移窗口规划图 |
| LunarTransferPlanner | 地月/月地转移规划 |
| TargetInterceptPlanner | 目标拦截窗口规划 |
| ResonantOrbitCalculator [2版本] | 共振轨道（星座部署）计算 |
| Trajectories | 再入大气落点预测 |
| BetterBurnTime | 精确点火倒计时/时间 |
| OrbitPOInts | 轨道兴趣点信息 |
| ManeuverNodeController | 机动节点精细编辑 |
| PreciseEditor | VAB 内精确平移/旋转部件 |
| DockingPortAlignmentIndicator [2版本] / DockingAlignmentDisplay / NavballDockAlignIndCE | 对接对准指示 |
| DockingCamKURS | KURS 式对接相机 |
| DockRotate | 对接口旋转控制 |
| KramaxAutoPilot / KramaxAutopilotContinued | 飞机自动驾驶（航线/自动着陆） |
| AtmosphereAutopilot | 电传操纵/增稳 |
| ThrottleControlledAvionics | 多引擎推力平衡（VTOL/精向着陆） |
| BoosterGuidance | 助推器动力回收制导 |
| SolarSailNavigator | 太阳帆航线规划 |
| PersistentThrust | 时间加速下保持低推力推进 |
| PersistentRotation / PersistentRotationUpgraded | 时间加速下保持自旋/指向 |
| TrimController / GimbalTrim | 飞机配平/引擎万向微调 |
| GPWS / GroundProximity | 近地警告 |
| FlightPlan [3版本] | 飞行计划工具 |
| KerbalGPSRevived | GPS 定位/导航 |
| KerbalTelemetry | 飞行遥测面板 |
| AntennaHelper / AntennaHelperNext | 天线覆盖/信号强度计算 |
| CommNext | 下一代通信网络模拟 |
| WhereCanIGo | 剩余 Δv 可达范围 |
| WindTunnel [2版本] | VAB 内气动风洞模拟 |
| CorrectCoL | 修正升力中心显示 |
| RCSBuildAid + RCSBuildAidCont | RCS 布置辅助（力矩显示） |
| EditorExtensionsRedux | 编辑器增强（角度吸附等） |
| HangarExtender / FShangarExtender / HangerExtenderExtended | 扩大 VAB/SPH 视野范围 |
| HangarGrid | VAB 参考网格 |
| WASDForVAB / WasdEditorCameraContinued | WASD 控制编辑器相机 |
| VABOrganizer [2版本] + Munk / RP-1 / SheepDog 配置包 | 部件列表整理/子分类 |
| JanitorsCloset [2版本] | 隐藏/整理不用的部件 |
| HideVAB | 隐藏 VAB 建筑以省性能 |
| BetterCrewAssignment | 乘员自动分配 |
| ShipManifest | 船内资源/乘员/科学管理 |
| CrewManifest / CrewQueueTwo / CrewFiles | 乘员名单管理 |
| EasyVesselSwitch | 点击切换载具 |
| VesselMoverContinued | 搬运/传送载具 |
| HyperEdit [2版本] | 作弊器（轨道编辑等） |
| LazyOrbit（KSP2） | 轨道直接设置 |
| BetterLoadSaveGame | 存档管理增强 |
| KerbalImprovedSaveSystem | 存档界面改进 |
| RecoveryController / StageRecovery [2版本] | 分级回收与回收款计算 |
| FMRSContinued | 飞行中回跳（助推器回收操作） |
| FlightTracker | 飞行记录统计 |
| KaptainsLog | 船长日志 |
| ScienceAlert / ScienceSituationInfo / ScienceArkive | 科学实验提醒/管理 |
| xScience / xScienceContinued | 科学点数总览 |
| SCANsat [2版本] | 地形/资源扫描卫星 |
| ContractParser / ContractsWindowPlus / ContractRewardModifier | 合同列表/窗口/奖励调整 |
| CapCom / MissionController2 | 合同任务管理界面 |
| WaypointManager | 自定义航点 |
| KerbalAlarmClock [5版本] / AlarmClock / StockAlarmClockDisabler [2版本] | 机动/转移闹钟（及禁用原版闹钟） |
| BetterTimeWarpCont [3版本] / TimeControl / SASed-Warp | 时间加速增强/慢动作 |
| Kronometer | 自定义日历时钟（RSS 必备） |
| RSSDateTimeFormatter | RSS 日期格式 |
| SpeedUnitAnnex | 更多速度单位 |
| QuickCursorHider | 自动隐藏鼠标 |
| ShowFPS | FPS 显示 |
| TooManyOrbits | 轨道线显示管理 |
| MapViewFocusTargeting | 地图视图聚焦目标 |
| Zoomer | 相机缩放增强 (?) |
| MouseFlyer | 鼠标直接控飞机 |
| CameraTools / CameraControllerNext / KerbalView | 相机模式增强 |
| QuickMods | 快捷 MOD 开关 (?) |
| IWishTheyMadeUICustomizable / BetterUI / BetterIcons / UIScaler | UI 自定义/缩放 |
| FlagTexts / PlusFlags / TriggerAu-Flags / JohnsCustomFlags / PhoenixIndustriesFlags / AtomicTechFlags-ATHSS / DKSalvageAgencyAndFlag / REPOSoftTech-Agencies / ASETAgency | 旗帜/机构包 |
| KerbalPlanetEmblems / OrbitIconsPack | 星球徽标/轨道图标 |
| DangerAlertsContinued | 危险警报 |
| AllYAll | 一键操作所有同类部件 |
| TweakableEverything / TweakableEverythingCont | 让所有部件选项可调 |
| VesselView | 载具 3D 可视化 |
| KVVContinued | 载具渲染截图（Kronal Vessel Viewer） |
| WheresMyCrewCapsule | 快速定位乘员舱 |
| CrossFeedEnabler | 跨部件燃料输送 |
| TacFuelBalancer | 燃料自动平衡 |
| Wacapella | 音乐播放 (?) |
| Meltdown | 核反应堆熔毁模拟 (?) |
| GravityIssue | 低重力行走问题修复 (?) |

## 四、现实主义生态（RSS/RO/RP-1/RemoteTech/Kerbalism）

| 模组 | 作用 |
|---|---|
| RealSolarSystem | 真实太阳系 |
| RSSTextures16K / RSSTextures8192 | RSS 高清地球贴图 |
| RSSOrigin / RSSOrigin2-Core / RSSOrigin-TopoRevampConfigs / -Textures16kPart2 | RSS 下一代地形/贴图 |
| RSS-Adapter / RSS-CanaveralHD | RSS 适配/高清卡角发射场 |
| RealismOverhaul [5版本] + RealismOverhaulCraftFiles + ROLoadingImages + RONoCareer | 现实主义大修及配套 |
| RP-1 [9版本] + RP-1-ExpressInstall + RP-1FullThrust + RP-1-Checklists + KerbalFunds-RP1 + VABOrganizer-RP-1 | 真实进度生涯模式及配套 |
| RealFuels [4版本] / ModularFuelTanks / RealFuels-Stock / RFStockalike | 真实燃料/油箱 |
| SolverEngines | 真实发动机性能求解（AJE/RF 依赖） |
| AdvancedJetEngine + AJEExtendedConfigs | 真实喷气发动机 |
| FerramAerospaceResearch / FerramAerospaceResearchContinued [各2版本] | 真实气动 |
| RealChute | 真实降落伞 |
| RealHeat | 真实再入热 |
| RealAntennas [2版本] | 真实天线（增益/频段） |
| RemoteTech + RemoteTechStockConfigs + RemoteTechBlackoutFix + SETI-RemoteTech + ContractConfigurator-RemoteTech | 信号延迟/无人控制需连接 |
| RealBattery | 真实电池特性 |
| RealismEnvironmentalOverhaul | 环境真实化 (?) |
| ROEngines + ROEnginesExtended / ROCapsules / ROTanks / ROSolar / ROLib [2版本] / ROHeatshields / ROUtils [2版本] | RO 配套真实部件库 |
| TestFlight [2版本] + TestFlightConfigLibrary + TestFlightConfig-Stock-S42-Revived [2版本] | 发动机可靠性/故障率系统 |
| KerbalConstructionTime [2版本] | 建造/翻修需要时间 |
| BARIS / BarisBridge | 载具与建筑状态/故障管理 (?) |
| ScrapYard [2版本] / OhScrap | 部件库存与复用折扣 |
| ProbesBeforeCrew [3版本] / UnmannedBeforeManned + Challenge | 先无人后载人科技线 |
| HistoricalProgressionTechTree | 历史顺序科技树 |
| BetterEarlyTree | 早期科技树优化 |
| ChemicalTechTree [2版本] + ChemicalCore [3版本] + ChemicalPropulsion [4版本] | 化学推进科技树/部件体系 |
| MakingAlternateHistory | 架空历史科技/部件 |
| SMURFF | 油箱干质比修正（接近现实） |
| LessRealThanReal / LessRealKerbalism | 降低真实度的折中包 |
| Ignition [3版本] / EngineIgniter / EngineIgnitorReignited | 发动机点火/ullage 模拟 |
| Kerbalism [5版本] + Kerbalism-Config-Default [5版本] / -Config-RO | 生命保障+辐射+科学流程大修 |
| KerbalismEngineFailures [2版本] / Kerbalism-FarFutureTechnologies / Kerbalism-SystemHeat / KerbalismLabExperimentsExpandedContinued / KerbalismCompatibilityOverhaul / KerbalismNFEFRpatch / SensorPackageforKerbalism / SIMPLEXKerbalism / UKS-Kerbalism-Patch | Kerbalism 的各类扩展/兼容补丁 |
| SpaceWeatherAndAtmosphericOrbitalDecay [4版本] / OrbitalDecay / OrbitalDecayNext | 轨道衰减/空间天气 |
| KesslerSyndrome / KessleractClient | 太空碎片（凯斯勒效应） |
| PersistentThrust | 见上，RO 常用 |
| Principia 不在缓存中（本机为单独安装） | — |

## 五、载具部件包

| 模组 | 作用 |
|---|---|
| NearFutureSolar/Electrical/Propulsion/Construction/Spacecraft/LaunchVehicles/Aeronautics/Exploration/Props | 近未来全家桶（Nertea） |
| FarFutureTechnologies | 远未来引擎（Nertea） |
| CryoEngines + CryoEnginesExtensions / CryoTanks | 低温引擎与储罐 |
| KerbalAtomics | 核热引擎 |
| HeatControl | 散热器 |
| SystemHeat [3版本] | 热管理系统 |
| DynamicBatteryStorage [2版本] | 动态电力/热管理 |
| B9AerospaceLegacy / B9AerospaceHX / B9-props / B9AerospaceProceduralParts / B9-PWings-Fork [2版本] | B9 飞机/机翼部件 |
| OPTSpacePlaneMain + OPTReconfig | OPT 空天飞机 |
| Mk2Expansion / Mk3Expansion / MarkIVSpaceplaneSystem | Mk2/Mk3/MarkIV 机体扩展 |
| AirplanePlus / QuizTechAeroPackContinued / SXTContinued | 飞机部件包 |
| NeistAirReupdated / ModeratelyPlaneRelated / PsiWedge [2版本] | 飞机类部件包 |
| ProceduralParts / ProceduralFairings / SimpleAdjustableFairings (+KWRocketry 版) | 程序化油箱/整流罩 |
| TweakScale / TweakScale-Redist / TweakScaleRescaled-Redist / TweakScaleCompanion [6版本] | 部件自由缩放 |
| ReStock / ReStockPlus / VenStockRevamp-Core | 原版部件重制 |
| BluedogDB [2版本] | 美式真实系火箭（BDB） |
| NewTantares / Taerobee | 苏联系飞船 / 早期火箭 |
| RN-SovietSpacecraft / RN-SalyutStations | 苏联飞船 / 礼炮空间站 |
| BuranEnergia | 能源号/暴风雪号 |
| KWRocketry + Rebalanced / LegacyFairings / CommunityFixes | KW 火箭 |
| SpaceY-Lifters / SpaceY-Expanded | 大推力火箭部件 |
| TundraExploration [2版本] / TundraSpaceCenter | SpaceX 猎鹰系及卡角场景 |
| StarshipExpansionProject [2版本] | 星舰 |
| SpaceXLegs / SpaceXLandingPad | 猎鹰着陆腿/着陆场 |
| ShuttleOrbiterConstructionKit / ShuttlePayloadDeliverySystems / ShuttleLiftingBodyCormorantAeronology | 航天飞机建造/载荷/升力体 |
| TheISSConstructionKit | ISS 建造套件 |
| HabTech / HabTech2 / HabTechProps / HabTechRobotics | ISS 风格空间站部件 |
| StationPartsExpansionRedux / SIMPLEXStationParts | 空间站部件 |
| PlanetaryDomes | 基地穹顶 |
| DeepSpaceSurfaceHabitatUnitPack | 深空地表居住舱 |
| KerbalPlanetaryBaseSystems | 行星基地系统 |
| Pathfinder [3版本] (WBI) | 充气/模块化基地 |
| Buffalo2 [3版本] (WBI) | 多用途探测车/飞行器 |
| MalemuteRover / FelineUtilityRovers / ExplorationRoverSystembyASET | 探测车 |
| KerBalloons / ColdJHotAirBalloon | 科研气球/热气球 |
| MOISTUnderwaterTechnologies / SunkWorks / WorkingUnderwaterLite | 水下科技/潜艇 |
| KerbalFoundriesContinued / KSPWheel / AdjustableLandingGear | 履带/轮子/可调起落架 |
| RetractableLiftingSurface | 可收放机翼 |
| InfernalRobotics / InfernalRoboticsNext / IR-LegacyParts / IR-Model-Rework-Core / IR-Kinematics / IR-ConnectionSystem / IR-Canadarm / InfernalRoboticsRealismOverhaul | 机械臂/活动部件（IR 全家） |
| KerbalActuators | 舵机/执行器 |
| KAS / KIS | 绞盘/管道连接 与 舱外库存 |
| UbioWeldContinued | 部件焊接（减 part 数） |
| Konstruction / GroundConstruction-Core / ExtraPlanetaryLaunchpads / SimpleConstruction / Sandcastle | 就地建造/外星发射台 |
| ModularLaunchPads [2版本] | 模块化发射台 |
| Hangar | 可收纳小载具的机库 |
| UniversalStorage / UniversalStorage2 [3版本] | 楔形多功能储物部件 |
| SimpleCargoSolutions | 货舱部件 |
| ConnectedLivingSpace | 乘员舱内通道 |
| FreeIva | 舱内自由行走 |
| DE-IVAExtension / Reviva | IVA 扩展/改进 |
| RasterPropMonitor-Core [3版本] | IVA 多功能显示屏 |
| ASETProps [2版本] / ASETAvionics | 高质量 IVA 设备 |
| ALCOR | ASET 风格座舱 IVA |
| Mk1CockpitIVAReplbyASET | Mk1 座舱 IVA 替换 |
| JSIAdvancedTransparentPods | 透明座舱盖 |
| ProbeControlRoomRecontrolled | 无人任务控制室 IVA |
| ThroughTheEyesOfaKerbal [2版本] | 第一人称视角 |
| HullcamVDSContinued | 船载摄像头 |
| AviationLights / IndicatorLights (+CommunityExtensions) / surfacelights / KerbalHeadlamp / CrewLight | 航行灯/指示灯/照明 |
| InternalRCS | 隐藏式 RCS |
| InlineBallutes | 充气减速 |
| DeployableEngines | 可展开引擎（猎鹰式） |
| PureElectricEngines / SupplementaryElectricEngines | 电推进 |
| SimpleNuclear / SimpleNuclear-NFT / TheNuclearOption | 核动力部件 |
| PhotonSailor / SillyPhotonDrives | 太阳帆/光压推进 |
| StockishProjectOrion | 猎户座核脉冲火箭 |
| KSPInterstellarExtended | 星际级引擎/能源 |
| InterstellarTechnologies | 星际科技部件 |
| Blueshift [5版本] | 曲速引擎（WBI） |
| SpaceDust [2版本] / SpaceDustNext | 大气/太空资源采集 |
| Karbonite | Karbonite 资源体系 |
| RationalResources [6版本] | 合理的资源/ISRУ 配置 |
| ClassicStockResources [2版本] | 经典资源体系（WBI） |
| ConfigurableContainers | 自由配置储罐内容 |
| SimpleFuelSwitch | 油箱燃料切换 |
| ODFC-Refueled | 燃料电池 |
| FuelVent-RP | 燃料泄放 |
| ModularRocketSystemsLITE / RLAReborn / RocketMotorMenagerie | 发动机/火箭部件包 |
| StreamlineEnginesRCSsandfueltanks | 简化引擎/RCS/油箱包 |
| SpacetuxSA / KomUniMunCW [2版本] / KOOSE | SpaceTux 系部件/机构 |
| CxAerospace / Coatl-Aerospace-Redux / TalisarParts / NehemiahMultiPurposeParts / MoldaviteMachines / MEVHeavyIndustries / PhoenixIndustriesAres / QuestariaAereospace / BigBlue / Ecumenopolis / Heracleitus / Krispy / Bumblebee / Droplet / GRAPEFRUIT / ProbablyAskew / TurboNisuFinalized / Leek / Fishy-Mod-Core / Labradoodle / KapybaraSpaceProgram2 / AlcoholicAeronautics / ColdWarAerospace / ShadoUFO / NiceMKseriesBody / ASSET-Mk1 / Corvus125mTwoKerbalPod / MiniSampleReturnCapsule / SterlingSystemsAgency/Engines/Thermals/Utilities / StructuralTubing / TokamakRefurbishedParts / GenericMoverPack / JxFabUtilitySystems / JakesKonstructStatics / LuxsFlamesandOrnaments / AblativeAirbrake / TALRadiallyAttachedExperimentalDataStorageContainer / RadialExperimentStorageContainerRecolor / Mk-33 / SPARKTechnologies / NuriLaunchExpansion / KonstellationHLV / PlanetsideExplorationTechnologies / MunarIndustriesFTX / RecycledPartskal9000 / DavonSupplyMod / DavonTCsystemsMod / CapsuleCorpKerbalKolonizationProgram / ColonyBlocksandPlatforms / OrdinaryKonstruction / CompletelyNonAggressiveRocketry / StockalikeStructures / StockishProjectOrion | 各类中小部件包（多为载具/基地/实验部件，冷门的按名称推测） |
| MoarKerbals / MoarKerbalsParts / MoreHitchhikers | 乘员舱/居住扩展 |
| KerbalizedSuits 等宇航服类见视觉区 | — |
| AviationLights 等见上 | — |

## 六、生涯 / 合同 / 游戏机制

| 模组 | 作用 |
|---|---|
| ContractConfigurator [6版本] | 合同包框架 |
| ├ AnomalySurveyor / CareerEvolution / CleverSats / Constellations / ExplorationPlus / FieldResearch / GrandTours / JNSQ-GAP / KerbalAcademy / KerbinSpaceStation / PlanesWithPurposes / RemoteTech / RoverMissionsRedux / SCANsat(×2) / Tourism / UnmannedContracts / ContractPackHistoricalProgression | 各自合同包：异常点调查、生涯演进、卫星星座、探索、外场研究、环球旅行、飞行学员、空间站、飞机任务、无人任务、历史任务进度等 |
| GAP (Giving Aircraft a Purpose) / ExtremePlaneContracts / FoxDefenseContracts | 航空/军事合同包 |
| Strategia / ReStrategia | 管理机构策略（替代原版策略） |
| Bureaucracy | 资金/声望/职员管理 |
| TourismExpanded | 太空旅游扩展 |
| KerbalRenamer / KerbalGenerations [2版本] / KerbonautManager / KerbalStats / TRPHire / EnhancedAstronautComplex (+合同配置) | 乘员命名/世代/属性/雇佣 |
| FinalFrontier / WayfarersWingsRankRibbons / KcalbelohFinalFrontierRibbonPack / STMsFFRibbonPackNavalOfficerRanks | 乘员勋章/勋表 |
| MemorialWall / DeceasedKerbals | 阵亡纪念 |
| KerbalHealth | 乘员健康系统 |
| DeepFreeze (V0.31/V1.32) | 乘员冷冻休眠 |
| Snacks / KerbalLifeSupportSystem / KICKLifeSupport / USI-LS | 生命保障（零食/氧气等） |
| ResearchBodies | 先发现天体才能去 |
| ResearchAdvancementDivision | 科研增强 (?) |
| ProgressParser | 科技树进度分析 |
| HideEmptyTechNodes | 隐藏空科技节点 |
| CommunityTechTree | 社区扩展科技树 |
| ImprovedTreeEnginePlacement | 更合理的引擎科技位置 |
| Komplexity | 生涯难度调整 |
| FundingFloor | 资金下限保护 |
| ScaledMissions | 任务规模缩放 (?) |
| MissionTreeExpanded | 任务线扩展 (?) |
| CivilianPopulation | 平民人口模拟 |
| GalacticTrading | 星际贸易 (?) |
| StationScienceContinued / SurfaceExperimentPack / DMagicOrbitalScience / DMagicScienceAnimate / KrakenScience / RoverScienceCont | 实验/科学玩法扩展 |
| KerbalKrashSystem / CrashKerbal / RandomPartExplosion | 碰撞损伤/部件爆炸 |
| SolarPanelDegradation | 太阳能板老化 |
| KerbalSoaring | 滑翔热气流模拟 |
| KerbalWeatherProject / Beaufort / Cloudy | 天气/云/海况 |
| EVAEnhancementsContinued / EvaFollower / EVAManager / EVARefueling / EVARepairs / G3MagnetBoots / WalkAbout | EVA 增强全家 |
| KerbalAnimationSuiteCont | 小绿人摆拍动画 |
| KerbalLifeHacks | 小技巧辅助 (?) |
| KerbalChecklists / RP-1-Checklists | 发射检查单 |
| KRASH | 付费模拟发射（先试后飞） |
| CustomPreLaunchChecks | 发射前检查定制 |
| CustomBarnKit | 自定义建筑升级数值 |
| KSCSwitcher | 多发射场切换 |
| KerbalKonstructs [4版本] + KerbinSideCore / KerbinSideRemastered (+GAP/TLA/GapExtras) + TopSecretBasesForKerbalKonstructs + LaunchSitesAppended + ActualSitesAirports + WaterLaunchSites + CanaveralPads | 静态基地/新发射场体系 |
| KSCExtended / KSCPlusPlus / KSCHarbor / VansKSC [2版本] / AuroraSpaceCenter / ScatteredCities / KerbalCitiesPack | KSC 与城市场景扩展 |
| TrackingStationEvolved | 追踪站增强 |
| MissionController2 / CapCom | 合同管理（见三区） |
| Kaboom | 手动/自动引爆部件 |
| SpringCleaning | 存档清理 (?) |
| ShipEngineOptimization | 引擎性能优化 (?) |
| SimpleLogistics | 基地间物流 (?) |
| Interkosmos | 苏联系生涯要素 (?) |
| KSPRescuePodFix / KSPRescueContractFix | 救援合同 bug 修复 |

## 七、武器 / 军事

| 模组 | 作用 |
|---|---|
| BDArmory / BDArmoryContinued / BDArmoryForRunwayProject [3版本] | 武器系统核心 |
| BDArmoryExtended [2版本] / BDArmoryWeaponsExtension / BDModularMissileParts / BDRailgun / RefurbishedArmory | 武器扩展（导弹/电磁炮等） |
| FishyAntiMissileSpam | 反导弹机制平衡 (?) |
| NorthKerbinDynamics / NorthKerbinDynamicsRenewedExtended | 核武器 |
| MNWSModernNavalWeaponSystem | 现代海军武器系统 |
| AviatorArsenal | 二战航空武器 |
| WorldWarMaritimeShips | 二战军舰 |
| ColdJsHeliCarrier | 直升机航母 |
| SinkEmAll | 舰船可沉没 |
| PhysicsRangeExtender | （依赖，见框架区） |

## 八、星球包 / 星系

| 模组 | 作用 |
|---|---|
| RealSolarSystem 全家 | 见现实主义区 |
| JNSQ + JNSQ-KSRGAP | 2.7x 精致原版风星系大修 |
| OuterPlanetsMod (OPM) + ScattererOPM + CommunityDeltaVMaps-OPM + CustomAsteroids-Pops-OPM-Reconfig | 外行星包及配套 |
| MinorPlanetsExpansion (MPE) | OPM 风格小行星带 |
| GrannusExpansionPack (GEP) | Grannus 红矮星星系 |
| KcalbelohSystem + Textures-8k + RibbonPack | 黑洞/双星星系 |
| PromisedWorldsCore / Debdeb / Tuun | KSP2 星球移植包 |
| OtherWorlds | 架空星球包 (?) |
| Sol 系列（Core/Configs/Visuals/Sun/Mercury(×2)/Venus/EarthSystem/MarsSystem/JupiterSystem/SaturnSystem(×2)/UranusSystem/NeptuneSystem/KuiperBelt/AsteroidBelt/Launchsites） | 高画质真实太阳系（16K 贴图） |
| AlmostRealSolarSystem | 简版真实太阳系 |
| 25xKerbolarSystem | 2.5 倍原版星系 |
| QuackPack | 外太阳系天体包 |
| CustomAsteroids | 自定义小行星群 |
| Paraterraforming | 星球地球化改造玩法 (?) |
| Planety | 小行星包 (?) |

## 九、KSP2 模组 / 联机

| 模组 | 作用 |
|---|---|
| BepInEx [2版本] / SpaceWarp / UITKforKSP2 / KSP2PAPI | KSP2 MOD 加载框架/UI 库 |
| K2D2 | KSP2 自动交会对接 |
| MicroEngineer | KSP2 飞行数据显示 |
| LazyOrbit | KSP2 轨道编辑 |
| ShowKSP2Events / KerbalSimpitKSP2 / PremonitionForSpaceWarp | KSP2 事件/外设/设置 |
| LunaMultiplayer | KSP1 联机 |
| KessleractClient | Kessler 碎片联机客户端 (?) |

## 十、中国题材

| 模组 | 作用 |
|---|---|
| KIUChinese-Common / KIUChineseHumanSpaceflightPack | 中国载人航天包 |
| NTChineseRocketsPack | 中国火箭包 |
| KerwisChinaAerospacePack | 中国航天部件包 |
| ChineseSpacecraftPack | 中国飞船包 |
| ChinesePackContinued | 中国包续作 (?) |
| ChineseModPatches [3版本] | 上述包与各 MOD 的兼容补丁 |

## 十一、其它 / 工具 / 难以归类

| 模组 | 作用 |
|---|---|
| KerbalChangelog | 见框架区 |
| KSPRescuePodFix / KSPRescueContractFix | 见生涯区 |
| USI-Core / USI-ART / USI-EXP / USI-FTT / USI-LS / USI-SRV / USITools / UKS / MKSPlusSSPX | MKS 殖民/物流/扩编体系（Umbra Space Industries） |
| WBI 系：WildBlueCore [3版本] / WildBlueTools [2版本] / WBIProps / WBIResources [2版本] / WBIScience [2版本] / WBIWidgets / MagpieMods [2版本] / AirlineKuisine | Wild Blue Industries 框架与部件 |
| MKSPlusSSPX | MKS 与 SSPX 联动 (?) |
| Kaboom / DangerAlertsContinued | 见上 |
| Droplet | 小型实验舱 (?) |
| ProbablyAskew | 趣味部件 (?) |
| TurboNisuFinalized | 飞机部件 (?) |
| KapybaraSpaceProgram2 | 趣味 MOD（水豚）(?) |
| AlcoholicAeronautics | 趣味飞机 MOD (?) |
| ShadoUFO | UFO 彩蛋 (?) |
| RandomPartExplosion | 见生涯区 |
| Mk-33 | 单级入轨空天飞机 (?) |
| QuestariaAereospace / Heracleitus / BigBlue / Ecumenopolis | 各作者部件包 (?) |
| Interkosmos | 苏联系组件 (?) |
| CommNext | 见飞行辅助区 |
| Zoomer / Wacapella / QuickMods / SpringCleaning | 小工具 (?) |


## 十二、补遗（首轮漏列）

| 模组 | 作用 |
|---|---|
| KerbalJointReinforcement / Continued [2版本] / Next | 节点关节加固，消除大型火箭晃动（重要物理修复） |
| QuantumStrutsContinued | 量子支撑杆，对接后固定防晃 |
| ReCoupler | 对接口自动多点耦合，消除空间站晃动 |
| AdaptiveDockingNode | 通用对接口（任意口径互相对接） |
| DockingFunctions | 对接口附加功能 (?) |
| PicoPort / PicoPortShielded | 微型对接口 |
| FirespitterCore | 螺旋桨/直升机/倾转旋翼模块 |
| ConstantSpeedProp | 恒速螺旋桨 |
| AvionicsSystems | IVA 航空电子系统（MAS 前身） |
| ModularComputerPackageRevived | 机载模块化计算机 (?) |
| BackgroundProcessing / BackgroundResources | 非活跃载具的后台计算/资源框架 |
| BOSSContinued | 非活跃载具后台简化模拟 (?) |
| BonVoyage [2版本] | 探测车后台自动巡航到目标点 |
| OrbitalKeeper [4版本] | 轨道碎片清理/管理 |
| OrbitalSurvey | 轨道勘测玩法/合同 (?) |
| Skopos | 通信卫星部署合同包 (?) |
| CactEyeCommunityRefocused | 太空望远镜/小行星观测科研 |
| TarsierSpaceTechnologyWithGalaxies | 太空望远镜（可观测星系） |
| JamesWebbForKerbal | 韦伯太空望远镜部件 |
| DeepSkyCore | Tarsier 系望远镜依赖核心 |
| JX2Antenna | 大型深空天线部件 |
| AerospacePassengerandUtilitySystem | 客机客舱/勤务部件 |
| AirParkContinued | 将载具固定悬停在空中（测试/拍摄用） |
| AnimatedDecouplers | 分离器分离动画 |
| AnimatedStationScreens | IVA/空间站屏幕动画 (?) |
| AutomatedAerialRefueling | 自动空中加油 |
| BetterBuoyancy | 浮力物理改进 |
| ComfortableLanding | 水上着陆浮筒/气囊部件 |
| DecouplerShroud [2版本] | 分离环整流罩 |
| DunaDirect | Duna 直达任务架构部件/玩法 (?) |
| FARGE | 飞行辅助 (?) |
| Grounded | 模块化车辆底盘 (?) |
| HUMANS | 人类乘员模型 (?) |
| KerbalField | 玩法扩展 (?) |
| KerbalMechanics | 部件磨损/维护机制 (?) |
| Kesa / KESASolar | KESA 太阳能板部件包 |
| KSAIVAUpgrade | KSA 风格 IVA 升级 (?) |
| KSIPlacementServices | KerbalKonstructs 静态放置服务 (?) |
| LCDLaunchCountDown / NASA-CountDown | 发射倒计时显示 |
| MoarFEConfigs | FilterExtensions 的更多部件分类配置 |
| NIMBY | 限制发射场附近的回收范围 |
| RadioactiveRemedialsLLC | 趣味机构/部件 (?) |
| RetroFuture | 复古未来风格部件包 |
| ScaledDecorator | KK 静态装饰缩放 (?) |
| Shoemaker | 小型工具 MOD (?) |
| StockalikeMiningExtension | 原版风格采矿扩展 |
| StorkDeliverySystem | 乘员/物资运送机制 (?) |
| UnKerballedStart | 纯无人开局的生涯模式 |
| VSwiFT | 飞行工具 (?) |
| WMCCModules | 部件模块库 (?) |
| BakerOperatingSystem | IVA 趣味操作系统界面 (?) |
| FuseBoxContinued | 全船电力负载实时显示（保险丝盒） |
| ColorPresets | 部件配色预设 (?) |
| AdvancedPQSTools | Kopernicus 地形开发工具 |
| UICore | UI 依赖库 |
| 贴纸/涂装类：Decalc-o-mania / DecalStickers / NEBULADecalsContinued / ConformalDecals [2版本] / IDFlagsandDecals / LazyPainter / SimpleRepaint | 机体贴花与涂装 |
