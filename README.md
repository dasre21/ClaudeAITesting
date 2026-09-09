# Playwright + SpecFlow + NUnit Test Framework

[![CI](https://github.com/dasre21/ClaudeAITesting/actions/workflows/ci.yml/badge.svg)](https://github.com/dasre21/ClaudeAITesting/actions/workflows/ci.yml)

Automation scripts created using Claude Code. A .NET test automation framework combining **Playwright** (browser automation), **SpecFlow** (Gherkin/BDD), **NUnit** (test runner/assertions), and the **Page Object Model** pattern.

## Folder structure

```
ClaudeAITesting/
├── PlaywrightSpecFlowFramework.slnx
├── Scripts/
│   └── Install-PlaywrightBrowsers.ps1   # builds the project and installs browser binaries
└── PlaywrightSpecFlow.Tests/
    ├── Config/
    │   ├── appsettings.json          # base configuration (BaseUrl, Browser, Headless, timeouts...)
    │   └── appsettings.dev.json       # environment overrides (selected via TEST_ENVIRONMENT)
    ├── Features/
    │   └── Login.feature              # Gherkin scenarios
    ├── StepDefinitions/
    │   └── LoginSteps.cs              # step definitions bound to Gherkin steps
    ├── Pages/
    │   ├── Base/
    │   │   └── BasePage.cs            # shared page actions (click, fill, navigate...)
    │   ├── Components/
    │   │   └── HeaderComponent.cs     # reusable UI fragment composed into pages
    │   ├── LoginPage.cs               # page object
    │   └── DashboardPage.cs           # page object
    ├── Hooks/
    │   └── Hooks.cs                   # BeforeScenario/AfterScenario: browser lifecycle, screenshots on failure
    ├── Support/
    │   ├── PlaywrightDriver.cs        # owns IPlaywright/IBrowser/IBrowserContext/IPage per scenario
    │   ├── TestSettings.cs            # strongly-typed configuration model
    │   └── ConfigReader.cs            # loads appsettings.json via Microsoft.Extensions.Configuration
    ├── Utilities/
    │   └── ScreenshotHelper.cs        # captures + attaches screenshots on failure
    ├── TestData/
    │   └── users.json                 # sample external test data
    └── specflow.json
```

## Prerequisites

- .NET SDK 10.0+
- PowerShell (for the browser install script)

## Setup

```powershell
dotnet restore
./Scripts/Install-PlaywrightBrowsers.ps1
```

## Running tests

```powershell
dotnet test
```

Run a subset by tag:

```powershell
dotnet test --filter "TestCategory=smoke"
```

Run against a different environment (loads `Config/appsettings.<name>.json` over the base file):

```powershell
$env:TEST_ENVIRONMENT = "dev"
dotnet test
```

## CI

`.github/workflows/ci.yml` runs on every push/PR to `main`: it restores, builds, installs the Chromium browser binary, and runs the full suite with `TEST_ENVIRONMENT=ci` (so it uses the headless base config rather than the headed `dev` override). Test results (`.trx`) and any failure screenshots are uploaded as workflow artifacts.

## How it fits together

- **SpecFlow** compiles `.feature` files into test fixtures and matches Gherkin steps to methods in `StepDefinitions/`.
- **`Hooks`** creates a fresh `PlaywrightDriver` (Playwright instance, browser, context, page) before each scenario and disposes it afterwards, capturing a screenshot when a scenario fails.
- **SpecFlow's dependency injection** (BoDi) resolves `PlaywrightDriver` as a single shared instance per scenario, so step classes and page objects constructor-inject it (or the page objects that use it) without any manual wiring.
- **Page objects** (`Pages/`) encapsulate locators and UI interactions behind business-facing methods (`LoginAsync`, `IsDisplayedAsync`, ...). Step definitions only call these methods — they never touch selectors directly.
- **`Config/appsettings.json`** drives the base URL, browser choice, headless mode, and timeouts; override per environment with `appsettings.<env>.json` and the `TEST_ENVIRONMENT` variable.

## Adapting the sample

The included `Login.feature` / `LoginPage` / `DashboardPage` target [saucedemo.com](https://www.saucedemo.com) purely as a working example. Replace the locators, base URL, and scenarios with your own application under test, following the same pattern:

1. Add a `.feature` file under `Features/`.
2. Add a page object under `Pages/` extending `BasePage`.
3. Add step definitions under `StepDefinitions/` that call the page object's methods.
