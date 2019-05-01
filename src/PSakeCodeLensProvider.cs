//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerShell.EditorServices.Commands;
using Microsoft.PowerShell.EditorServices.Symbols;

namespace Microsoft.PowerShell.EditorServices.CodeLenses
{
    public class PSakeCodeLensProvider : FeatureProviderBase, ICodeLensProvider
    {
        /// <summary>
        /// The symbol provider to get symbols from to build code lenses with.
        /// </summary>
        private IDocumentSymbolProvider _symbolProvider;

        /// <summary>
        /// Create a new PSake CodeLens provider for a given editor session.
        /// </summary>
        /// <param name="editorSession">The editor session context for which to provide PSake CodeLenses.</param>
        public PSakeCodeLensProvider()
        {
            _symbolProvider = new PSakeDocumentSymbolProvider();
        }

        /// <summary>
        /// Get the PSake CodeLenses for a given PSake symbol.
        /// </summary>
        /// <param name="psakeSymbol">The PSake symbol to get CodeLenses for.</param>
        /// <param name="scriptFile">The script file the PSake symbol comes from.</param>
        /// <returns>All CodeLenses for the given PSake symbol.</returns>
        private CodeLens[] GetPSakeLens(PSakeSymbolReference psakeSymbol, ScriptFile scriptFile)
        {
            var command = "Invoke-Build";
            var args = new string[] { 
                "-Task", psakeSymbol.TaskName, 
                "-File", scriptFile.FilePath };
            if (_symbolProvider is PSakeDocumentSymbolProvider psakeDocumentSymbolProvider
                && psakeDocumentSymbolProvider.BuildRunner == PSakeDocumentSymbolProvider.BuildRunnerOptions.PSake)
            {
                command = "Invoke-psake";
                args = new string[] { 
                    "-buildFile", scriptFile.FilePath,
                    "-taskList", psakeSymbol.TaskName };
            }

            var codeLensResults = new CodeLens[]
            {
                new CodeLens(
                    this,
                    scriptFile,
                    psakeSymbol.ScriptRegion,
                    new ClientCommand(
                        "PowerShell.RunCode",
                        "Run task",
                        new object[] {
                            false /* No debug */,
                            command,
                            args })),

                new CodeLens(
                    this,
                    scriptFile,
                    psakeSymbol.ScriptRegion,
                    new ClientCommand(
                        "PowerShell.RunCode",
                        "Debug task",
                        new object[] {
                            true /* Run in the debugger */,
                            command,
                            args })),
            };

            return codeLensResults;
        }

        /// <summary>
        /// Get all PSake CodeLenses for a given script file.
        /// </summary>
        /// <param name="scriptFile">The script file to get PSake CodeLenses for.</param>
        /// <returns>All PSake CodeLenses for the given script file.</returns>
        public CodeLens[] ProvideCodeLenses(ScriptFile scriptFile)
        {
            var lenses = new List<CodeLens>();
            foreach (SymbolReference symbol in _symbolProvider.ProvideDocumentSymbols(scriptFile))
            {
                if (symbol is PSakeSymbolReference psakeSymbol)
                {
                    lenses.AddRange(GetPSakeLens(psakeSymbol, scriptFile));
                }
            }

            return lenses.ToArray();
        }

        /// <summary>
        /// Resolve the CodeLens provision asynchronously -- just wraps the CodeLens argument in a task.
        /// </summary>
        /// <param name="codeLens">The code lens to resolve.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The given CodeLens, wrapped in a task.</returns>
        public Task<CodeLens> ResolveCodeLensAsync(CodeLens codeLens, CancellationToken cancellationToken)
        {
            // This provider has no specific behavior for
            // resolving CodeLenses.
            return Task.FromResult(codeLens);
        }
    }
}
