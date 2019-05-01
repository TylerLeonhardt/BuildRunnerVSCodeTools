# BuildRunnerVSCodeTools

A collection of tools for making the PSake & InvokeBuild experience in VSCode better!

## Tools included:

### `Run task` and `Debug task` CodeLenses that appear above tasks in your script:

#### PSake
![psake](https://user-images.githubusercontent.com/2644648/57004367-60c63100-6b83-11e9-94d5-a15d52742306.png)

#### InvokeBuild
![InvokeBuild](https://user-images.githubusercontent.com/2644648/57004404-ac78da80-6b83-11e9-860e-bd0171855581.png)

## Roadmap

### Auto-generate `tasks.json` files from build scripts so that you can run tasks [right from VSCode's UI](https://code.visualstudio.com/Docs/editor/tasks).

## Installation

### Dependencies

This module depends on you having Visual Studio Code installed, and the [*PowerShell Preview*](https://marketplace.visualstudio.com/items?itemName=ms-vscode.PowerShell-Preview) extension for VSCode.

> IMPORTANT: This will not work with the current PowerShell stable extension!

### PowerShell Gallery

```powershell
Install-Module BuildRunnerVSCodeTools
```

Then, import it in your *PowerShell Integrated Console* session for the CodeLens to take effect:
```powershell
Import-Module BuildRunnerVSCodeTools
```

For best performance, add that line to your PowerShell profile, or use the built-in `Add-BuildRunnerProfileScript` to append it to the end of your Profile.

You should be able to click on a `psakefile.ps1` or `*.build.ps1` and see the CodeLens!

### Build locally

1. `Invoke-Build`

### Running locally

1. `Import-Module ./path/to/BuildRunnerVSCodeTools/module/BuildRunnerVSCodeTools.psd1`

You should be able to click on a `psakefile.ps1` or `*.build.ps1` and see the CodeLens!

> NOTE: When running locally, the `Add-BuildRunnerProfileScript` won't use the path passed in to Import-Module
