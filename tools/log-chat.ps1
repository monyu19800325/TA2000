param(
  [Parameter(Mandatory = $true)]
  [string]$Text
)

$ErrorActionPreference = "Stop"

$projectRoot = Resolve-Path -Path .
$logRoot = Join-Path $projectRoot "chat_logs"
$day = Get-Date -Format "yyyy-MM-dd"
$logPath = Join-Path $logRoot "$day.txt"

New-Item -ItemType Directory -Force -Path $logRoot | Out-Null

$entry = "[{0}]\r\n{1}\r\n" -f (Get-Date -Format "yyyy-MM-dd HH:mm:ss"), $Text
$entry | Add-Content -Path $logPath -Encoding utf8

Write-Host "Chat log appended: $logPath"
