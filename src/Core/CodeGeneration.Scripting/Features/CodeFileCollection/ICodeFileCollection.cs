namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using CodeFiles;
    using System.Collections.Generic;

    /// <summary>
    /// Feature for <see cref="IScript"/>s that will generate C# code files.
    /// </summary>
    public interface ICodeFileCollection : IFeature, IEnumerable<(Identity<string> codeFileId, ICodeFile codeFile)>
    {
        /// <summary>
        /// Creates a new or gets an already existing <see cref="ICodeFile"/> by id.
        /// </summary>
        /// <param name="codeFileId"> The unique id of the script file to create or get. </param>
        /// <returns> The requested <see cref="ICodeFile" />. </returns>
        ICodeFile CreateOrGetFile(Identity<string> codeFileId);
    }
}