<#
.SYNOPSIS
    Builds the test project and installs the Playwright browser binaries.
.PARAMETER Configuration
    Build configuration to use (Debug or Release). Defaults to Debug.
#>
param(
    [string]$Configuration = "Debug"
)

$ErrorActionPreference = "Stop"

$projectDir = Join-Path $PSScriptRoot "..\PlaywrightSpecFlow.Tests"

Write-Host "Building test project ($Configuration)..." -ForegroundColor Cyan
dotnet build $projectDir -c $Configuration

$playwrightScript = Get-ChildItem -Path (Join-Path $projectDir "bin\$Configuration") -Recurse -Filter "playwright.ps1" |
    Select-Object -First 1

if (-not $playwrightScript) {
    throw "playwright.ps1 was not found under bin\$Configuration. Ensure the build succeeded."
}

Write-Host "Installing Playwright browsers..." -ForegroundColor Cyan
& $playwrightScript.FullName install --with-deps

Write-Host "Done." -ForegroundColor Green
