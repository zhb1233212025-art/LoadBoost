#!/usr/bin/env bash
set -euo pipefail
SRC_DIR="$(cd "$(dirname "$0")/.." && pwd)"
# 默认部署到主安装;参数可指定其它 KSP 根目录
DST="${1:-/e/ksp/Kerbal Space Program}/GameData/EngineLifecycle"
mkdir -p "$DST"
cp "$SRC_DIR/EngineLifecycle/bin/Release/net481/EngineLifecycle.dll" "$DST/"
cp "$SRC_DIR/GameData/EngineLifecycle/EngineLifecycle.cfg" "$DST/"
echo "Deployed to $DST"
