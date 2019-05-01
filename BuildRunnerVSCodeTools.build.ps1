Task Restore -If {!(Test-Path $PSScriptRoot/lib/PowerShellEditorServices)} {
    & "$PSScriptRoot/tools/AssertPSES.ps1"
}

Task Build Restore, {
    exec { dotnet publish $PSScriptRoot/src }

    $outDir = Join-Path $PSScriptRoot module BuildRunnerVSCodeTools bin
    if(Test-Path $outDir) {
        Remove-Item $outDir -Recurse -Force
    }
    mkdir $outDir

    Copy-Item $PSScriptRoot/src/bin/Debug/netstandard2.0/publish/BuildRunnerVSCodeTools.* $outDir
}

Task Clean {
    Remove-Item -Force -Recurse (Join-Path $PSScriptRoot module BuildRunnerVSCodeTools bin) -ErrorAction SilentlyContinue
    Remove-Item -Force -Recurse (Join-Path $PSScriptRoot src bin) -ErrorAction SilentlyContinue
    Remove-Item -Force -Recurse (Join-Path $PSScriptRoot src obj) -ErrorAction SilentlyContinue
    Remove-Item -Force -Recurse (Join-Path $PSScriptRoot lib) -ErrorAction SilentlyContinue
}

Task . Clean,Build
