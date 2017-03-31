<#
.SYNOPSIS
    Packages a Release nuget package of AsyncEventLib
.DESCRIPTION
    Runs "dotnet pack" to generate a nuget package for AsyncEventLib.  The package will
    be placed in the AsyncEventLib/nupkg folder.  Also generates a symbols package in the
    same folder.
.PARAMETER Version
    The nuget version you would like to use. Can optionally start with a lowercase
    "v", must be of the form <number>.<number>.<number> for release packages or of the form
    <number>.<number>.<number>-<characters (except dots '.')> for prerelease packages
.EXAMPLE
    C:\PS> publish.ps1 v0.1.12 -NoBuild
    Packages AsyncEventLib Release build under <project root folder>/AsyncEventLib/nupkg
.NOTES
    Author: Wes Peter
    Date:   April 1, 2017
#>
Param(
    [Parameter(Mandatory=$True,Position=1)]
    [string]$Version
)

$ErrorActionPreference = "Stop"

# Nuget release versions are 3 segments, all digits.
# Prerelease versions add a -foo to last segmeent, but no dots
# allowed though so -foo.23 not allowed, but -foo23 is
#
# https://docs.microsoft.com/en-us/nuget/create-packages/prerelease-packages
#
# Examples (reverse precedence order, so higher means later release)
# 1.0.1
# 1.0.1-zzz
# 1.0.1-rc
# 1.0.1-open
# 1.0.1-beta
# 1.0.1-alpha2
# 1.0.1-alpha

"Passed in version $Version"

if($Version.StartsWith("v"))
{
    $Version = $Version.Remove(0,1);
}

if(!($Version -match '^\d+\.\d+\.\d+(-\w+)?$')) {
    throw "did not match pattern of <number>.<number>.<number> or <number>.<number>.<number>-<word>"
}

$scriptsFolder = Get-Item $PSScriptRoot;
$projectRootFolder = $scriptsFolder.Parent;

$projectPath = Join-Path $projectRootFolder.FullName "AsyncEventLib" | Join-Path -ChildPath "AsyncEventLib.csproj";

"packing $projectPath"
# When specifying a msbuild parameter "/p:" with dotnet pack it only works on Mac, not on Windows or Linux. TODO: file a bug
# dotnet pack $projectPath --include-symbols --include-source --output nupkg --configuration Release $NoBuild /p:PackageVersion=$Version
dotnet msbuild $projectPath /t:pack /p:Version=$Version /p:IncludeSource=true /p:IncludeSymbols=true /p:Configuration=Release
if(!$?) { exit $LASTEXITCODE; }

# TODO: upload to nuget