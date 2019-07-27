namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using Code;
    using Composition;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An <see cref="IFeatureAnalyzer"/> that checks if an <see cref="IScript"/>'s source code
    /// generates <see cref="ICodeFile"/>s by calling methods on the global <see cref="IScriptContext.Code"/>.
    /// </summary>
    [Export]
    public sealed class CodeCollectionAnalyzer : IFeatureAnalyzer
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="CodeCollectionAnalyzer"/> type.
        /// </summary>
        /// <param name="featureFactory">
        /// A factory that can create a new instance of the <see cref="ICodeFileCollection"/> feature on demand.
        /// </param>
        public CodeCollectionAnalyzer(Func<ICodeFileCollection> featureFactory)
        {
            FeatureFactory = featureFactory;
        }

        /// <summary>
        /// Gets a factory that can create a new instance of the <see cref="ICodeFileCollection"/> feature on demand.
        /// </summary>
        private Func<ICodeFileCollection> FeatureFactory { get; }

        #endregion

        #region Data

        /// <summary>
        /// The name of the script's code context.
        /// </summary>
        private const string CodeContext = nameof(ScriptContext.Code);

        #endregion

        #region Logic

        /// <summary>
        /// Query if the given <paramref name="sourceCode"/> uses the <see cref="ICodeFileCollection"/> feature,
        /// i.e. if the source code makes calls to any of the <see cref="ScriptContext.Code"/> methods.
        /// </summary>
        /// <param name="sourceCode"> The script's source code (as Roslyn <see cref="SyntaxTree"/> representation). </param>
        /// <param name="feature"> The script's <see cref="ICodeFileCollection"/> feature (if present) or null otherwise. </param>
        /// <returns> True if the <see cref="ICodeFileCollection"/> feature was found, false otherwise. </returns>
        public bool HasFeature(IEnumerable<SyntaxTree> sourceCode, out IFeature feature)
        {
            if (sourceCode == null || !sourceCode.Any())
            {
                feature = null;
                return false;
            }

            var useCodeCollection = false;
            foreach (var syntaxTree in sourceCode)
            {
                var root = syntaxTree.GetRoot();
                var memberNodes = root.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                if (memberNodes == null || memberNodes.Count == 0)
                {
                    continue;
                }

                var codeNodes = memberNodes.Where(n => n
                    .DescendantNodes()
                    .OfType<IdentifierNameSyntax>()
                    .Any(name => name.Identifier.Text.Equals(CodeContext, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
                if (codeNodes == null || codeNodes.Count == 0)
                {
                    continue;
                }

                useCodeCollection = true;
                break;
            }

            feature = useCodeCollection ? FeatureFactory() : null;
            return useCodeCollection;
        }

        #endregion
    }
}