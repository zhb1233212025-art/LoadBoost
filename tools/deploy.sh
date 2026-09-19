#!/usr/bin/env bash
set -euo pipefail
SRC_DIR="$(cd "$(dirname "$0")/.." && pwd)"
# 默认部署到主安装;参数可指定其它 KSP 根目录(如 /e/ksp/Kerbal Space Program RP-1 solRO),也可用环境变量 KSPDIR 覆盖默认
DST="${1:-${KSPDIR:-/e/SteamLibrary/steamapps/common/Kerbal Space Program}}/GameData/LoadBoost"
mkdir -p "$DST/PluginData"
cp "$SRC_DIR/LoadBoost/bin/Release/net48/LoadBoost.dll" "$DST/"
cp "$SRC_DIR/LoadBoost/bin/Release/net48/LoadBoost.Core.dll" "$DST/"
# 清理旧版配置(曾用名 settings.cfg,.cfg 扩展名会触发 ModuleManager 缓存失效)
rm -f "$DST/settings.cfg"
# 配置文件不覆盖用户改动(A/B 实验需要),仅不存在时拷贝
cp -n "$SRC_DIR/GameData/LoadBoost/LoadBoostSettings.txt" "$DST/"
echo "Deployed to $DST"
