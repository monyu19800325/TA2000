README FIRST - TA2000 Project

啟動守則 (每日讀取專案時請先閱讀)
1. 每天開啟/讀取專案前，先閱讀本檔案 (README_FIRST.txt)。
2. 任何檔案需要修改時，先備份、再修改。
3. 對話紀錄需備份 (支援繁體中文，使用 UTF-8 編碼)。
4. 備份檔案與紀錄請存放於 backups/ 或 chat_logs/。

快速指令 (PowerShell)
- 讀取啟動守則：
  .\tools\readme-first.ps1

- 備份檔案或資料夾：
  .\tools\backup.ps1 -Path "相對或完整路徑" -Reason "修改原因"

- 備份對話紀錄：
  .\tools\log-chat.ps1 -Text "貼上對話紀錄"
