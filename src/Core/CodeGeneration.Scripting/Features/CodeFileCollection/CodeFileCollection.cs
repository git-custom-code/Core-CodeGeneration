namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using CodeFiles;
    using Composition;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Feature for <see cref="IScript"/>s that will generate C# code files.
    /// </summary>
    [Export(typeof(ICodeFileCollection))]
    public sealed class CodeFileCollection : LinkedFeature, ICodeFileCollection, IMutableCodeFileCollection
    {
        #region Data

        /// <summary>
        /// Gets the script's generated code files.
        /// </summary>
        private ConcurrentDictionary<string, ICodeFile> CodeFiles { get; set; } = new ConcurrentDictionary<string, ICodeFile>();

        #endregion

        #region Logic

        /// <inheritdoc />
        public ICodeFile CreateOrGetFile(Identity<string> codeFileId)
        {
            return CodeFiles.GetOrAdd(codeFileId.Value, id => new CodeFile());
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<(Identity<string> codeFileId, ICodeFile codeFile)> GetEnumerator()
        {
            foreach (var codeFile in CodeFiles)
            {
                yield return (new Identity<string>(codeFile.Key), codeFile.Value);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{CodeFiles.Count} generated code files";
        }

        /// <inheritdoc />
        public void UpdateValues(IEnumerable<(Identity<string> codeFileId, ICodeFile codeFile)> generatedCodeFiles)
        {
            CodeFiles = new ConcurrentDictionary<string, ICodeFile>(
                generatedCodeFiles.Select(f => new KeyValuePair<string, ICodeFile>(f.codeFileId.Value, f.codeFile)));
        }

        #endregion
    }
}