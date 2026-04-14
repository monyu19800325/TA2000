param(
  [Parameter(Mandatory = $true)]
  [string]$Path,
  [string]$Reason = "manual backup"
)

$ErrorActionPreference = "Stop"

$projectRoot = Resolve-Path -Path .
$backupRoot = Join-Path $projectRoot "backups"
$stamp = Get-Date -Format "yyyyMMdd"
$destDir = Join-Path $backupRoot $stamp

New-Item -ItemType Directory -Force -Path $destDir | Out-Null

$sourcePath = Resolve-Path -Path $Path
$sourceName = Split-Path -Leaf $sourcePath
$timeTag = Get-Date -Format "HHmmss"
$destName = "{0}_{1}" -f $timeTag, $sourceName
$destPath = Join-Path $destDir $destName

Copy-Item -Path $sourcePath -Destination $destPath -Recurse -Force

$logPath = Join-Path $destDir "backup-log.txt"
$logLine = "{0} | {1} | {2} -> {3}" -f (Get-Date -Format "yyyy-MM-dd HH:mm:ss"), $Reason, $sourcePath, $destPath
$logLine | Add-Content -Path $logPath -Encoding utf8

Write-Host "Backup created: $destPath"
