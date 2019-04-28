Task Restore -If(!(Test-Path $PSScriptRoot/lib)) {
    & "$PSScriptRoot/tools/AssertPSES.ps1"
}

Task Build Restore, {
    exec { dotnet publish $PSScriptRoot/src }

    $outDir = Join-Path $PSScriptRoot module bin
    if(Test-Path $outDir) {
        Remove-Item $outDir -Recurse -Force
    }
    mkdir $outDir

    Copy-Item $PSScriptRoot/bin/Debug/netstandard2.0/publish/InvokeBuildCodeLens.* $outDir
    Copy-Item $PSScriptRoot/InvokeBuildCodeLens.ps*1 $outDir
}

Task . Build
