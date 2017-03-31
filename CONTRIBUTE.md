# Contribute

## Prereqs to build
- [Dotnet SDK 1.0.1](https://www.microsoft.com/net/download/core#/sdk)
- [Powershell](https://github.com/PowerShell/PowerShell#get-powershell)

## How to pack nuget package
- Prerelease:
    ```
    powershell scripts/publish.ps1 v0.1.1-alpha20170121
    ```
- Release:
    ```
    powershell scripts/publish.ps1 v0.1.1-alpha20170121
    ```

## How to publish via git tags (must be repo admin)
```
git tag -a v0.1.12 -m "fixed some bug"
git push origin v0.1.12
```

TravisCI will read the tag and run the publish script to upload to nuget