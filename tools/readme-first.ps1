$ErrorActionPreference = "Stop"

$projectRoot = Resolve-Path -Path .
$readmePath = Join-Path $projectRoot "README_FIRST.txt"

if (-not (Test-Path $readmePath)) {
  throw "README_FIRST.txt not found."
}

Get-Content -Path $readmePath -Encoding utf8
