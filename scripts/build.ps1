$ErrorActionPreference = "Stop"

$scriptsFolder = Get-Item $PSScriptRoot;
$projectRootFolder = $scriptsFolder.Parent;

$testCsproj = Join-Path $projectRootFolder.FullName "AsyncEventLib.Tests" | Join-Path -ChildPath "AsyncEventLib.Tests.csproj";

"restoring packages"
dotnet restore
if(!$?) { exit $LASTEXITCODE; }

"running tests"
dotnet test "$testCsproj"
if(!$?) { exit $LASTEXITCODE; }

"building solution"
dotnet build --configuration Release
if(!$?) { exit $LASTEXITCODE; }