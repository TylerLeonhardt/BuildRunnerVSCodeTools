if(!$psEditor) {
    throw "Import into the PowerShell Integrated Console."
}
$psEditor.Components.Get([Microsoft.PowerShell.EditorServices.CodeLenses.ICodeLenses]).Providers.Add([Microsoft.PowerShell.EditorServices.CodeLenses.PSakeCodeLensProvider]::new())


function Add-BuildRunnerProfileScript {
    [CmdletBinding()]
    param (
        # Parameter help description
        [Parameter()]
        [ValidateSet("AllUsersAllHosts", "AllUsersCurrentHost", "CurrentUserAllHosts", "CurrentUserCurrentHost")]
        $ProfileType = "CurrentUserCurrentHost"
    )
    
    "Import-Module BuildRunnerVSCodeTools" | Out-File -FilePath $profile.$ProfileType -Append -Force
}
