if(!$psEditor) {
    throw "Import into PowerShell Integrated Console."
}
$psEditor.Components.Get([Microsoft.PowerShell.EditorServices.CodeLenses.ICodeLenses]).Providers.Add([Microsoft.PowerShell.EditorServices.CodeLenses.PSakeCodeLensProvider]::new())
