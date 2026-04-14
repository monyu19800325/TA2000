# TA2000

## 啟動守則
每天開始工作前，請先閱讀 `README_FIRST.txt`，或直接執行：

```powershell
.\Start-TA2000.ps1
```

如果要用批次檔：

```bat
Start-TA2000.cmd
```

## 修改前備份
修改任何檔案前，先備份：

```powershell
.\tools\backup.ps1 -Path "相對或完整路徑" -Reason "修改原因"
```

## 對話紀錄備份
每次對話結束或重要節點，請備份對話紀錄 (UTF-8)：

```powershell
.\tools\log-chat.ps1 -Text "貼上對話紀錄"
```

## 規則文件
詳細規範請見：
- `docs\STARTUP_RULES.md`
