namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using CodeFiles;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for the implementation detail, that the <see cref="IScriptRunner"/> needs to update
    /// the generated code files after the <see cref="IScript"/> has been executed.
    /// </summary>
    internal interface IMutableCodeFileCollection : IFeature
    {
        /// <summary>
        /// Update the script's generated code files.
        /// </summary>
        /// <param name="generatedCodeFiles">
        /// The generated code files from the global <see cref="ScriptContext.Code"/> after the
        /// <see cref="IScript"/> has been executed.
        /// </param>
        void UpdateValues(IEnumerable<(Identity<string> codeFileId, ICodeFile codeFile)> generatedCodeFiles);
    }
}