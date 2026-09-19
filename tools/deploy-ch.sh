#!/usr/bin/env bash
set -euo pipefail
SRC_DIR="$(cd "$(dirname "$0")/.." && pwd)"
# 默认部署到主安装;参数可指定其它 KSP 根目录,也可用环境变量 KSPDIR 覆盖默认
DST="${1:-${KSPDIR:-/e/SteamLibrary/steamapps/common/Kerbal Space Program}}/GameData/CrewHiring"
mkdir -p "$DST"
cp "$SRC_DIR/CrewHiring/bin/Release/net481/CrewHiring.dll" "$DST/"
cp "$SRC_DIR/GameData/CrewHiring/Kolonists.cfg" "$DST/"
echo "Deployed to $DST"
