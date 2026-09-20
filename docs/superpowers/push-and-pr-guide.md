# LoadBoost 推送 GitHub + 建 PR 指南（2026-09-20）

当前状态：功能已合并到 `master`（7 提交），测试 14/14 绿，发布 zip 已打好。仓库**尚无 GitHub 远程、无凭据、未装 gh**。

## 已备好的产物

- **发布包**：`dist/LoadBoost-v0.1.1.zip`（GameData/LoadBoost 下 LoadBoost.dll + LoadBoost.Core.dll + LoadBoostSettings.txt，解压到 KSP 根即用）
- **README**：仓库根 `README.md`（中英双语，建仓库后即为项目主页）
- **PR 正文草稿**：`docs/superpowers/pr-body.md`（推送后建 PR 时粘贴）

## 你要做的（一次性，约 3 分钟）

### 1. 装 gh + 登录
```powershell
winget install --id GitHub.cli
# 装完重开终端，然后登录（弹浏览器授权）：
gh auth login
```

### 2. 建公开仓库并推送
```powershell
cd E:\仓库
gh repo create LoadBoost --public --source=. --remote=origin --push
```
（一条命令同时建仓库 `LoadBoost`、加 origin 远程、把当前 master 推上去。）

### 3. 建 Pull Request（可选）
本仓库是你个人单分支项目，欢迎窗功能**已直接合并进 master**，其实不必再开 PR。若你想要 PR 流程（比如以后多人协作/留记录），推送后可对某个功能分支建 PR，正文用 `docs/superpowers/pr-body.md`。

### 4. 发 Release（发布模组）
```powershell
cd E:\仓库
gh release create v0.1.1 "dist/LoadBoost-v0.1.1.zip" --title "LoadBoost v0.1.1" --notes-file docs/superpowers/release-notes.md
```

## 备注
- git 身份未全局配置；本次提交作者是子代理临时配置。推送前建议设一次：
  ```powershell
  git config --global user.name "你的名字"
  git config --global user.email "你的邮箱"
  ```
- 若 push 报 dubious ownership，已加过 safe.directory，正常不会再报。
