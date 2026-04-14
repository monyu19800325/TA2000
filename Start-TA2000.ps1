$ErrorActionPreference = "Stop"

$projectRoot = Resolve-Path -Path .
$readmeScript = Join-Path $projectRoot "tools\readme-first.ps1"

if (-not (Test-Path $readmeScript)) {
  throw "tools\\readme-first.ps1 not found."
}

& $readmeScript

Write-Host "\nTips:" 
Write-Host "- Backup before changes: .\\tools\\backup.ps1 -Path \"path\" -Reason \"reason\"" 
Write-Host "- Log chat: .\\tools\\log-chat.ps1 -Text \"...\"" 
