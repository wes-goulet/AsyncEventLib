$ErrorActionPreference = "Stop"

$scriptsFolder = Get-Item $PSScriptRoot;
$projectRootFolder = $scriptsFolder.Parent;

$libFolder = Join-Path $projectRootFolder.FullName "AsyncEventLib"

"packing"
dotnet pack "$libFolder" --include-symbols --include-source
if(!$?) { exit $LASTEXITCODE; }