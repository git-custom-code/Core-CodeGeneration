namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using Composition;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An <see cref="IFeatureAnalyzer"/> that analyzes if an <see cref="IScript"/> source code
    /// uses any <see cref="Modelling.IModel"/>.
    /// </summary>
    [Export]
    public sealed class ModelCollectionAnalyzer : IFeatureAnalyzer
    {
        #region Data

        /// <summary>
        /// The name of the script's <see cref="Modelling.IO.IModelRepository"/> context.
        /// </summary>
        private const string ModelContext = nameof(ScriptContext.Models);

        #endregion

        #region Logic

        /// <summary>
        /// Query if the <paramref name="sourceCode"/> uses the <see cref="ModelCollection"/> feature, i.e. if the
        /// script makes calls to any of the <see cref="ScriptContext.Models"/>.
        /// </summary>
        /// <param name="sourceCode"> The script's source code (as Roslyn <see cref="SyntaxTree"/> representation). </param>
        /// <param name="feature"> The script's <see cref="ModelCollection"/> feature (if present) or null otherwise. </param>
        /// <returns> True if the <see cref="ModelCollection"/> feature was found, false otherwise. </returns>
        public bool HasFeature(IEnumerable<SyntaxTree> sourceCode, out IFeature feature)
        {
            if (sourceCode == null || !sourceCode.Any())
            {
                feature = null;
                return false;
            }

            var modelPaths = new List<string>();
            foreach (var syntaxTree in sourceCode)
            {
                var root = syntaxTree.GetRoot();
                var invocationNodes = root.DescendantNodes().OfType<InvocationExpressionSyntax>().ToList();
                if (invocationNodes == null || invocationNodes.Count == 0)
                {
                    continue;
                }

                var modelNodes = invocationNodes.Where(n => n
                    .DescendantNodes()
                    .OfType<IdentifierNameSyntax>()
                    .Any(name => name.Identifier.Text.Equals(ModelContext, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
                if (modelNodes == null || modelNodes.Count == 0)
                {
                    continue;
                }

                var paths = modelNodes.SelectMany(n => n
                    .DescendantNodes()
                    .OfType<LiteralExpressionSyntax>()
                    .Select(l => l.Token.ValueText))
                    .ToList();
                foreach(var path in paths)
                {
                    modelPaths.Add(path);
                }
            }

            feature = modelPaths.Count > 0 ? new ModelCollection(modelPaths) : null;
            return modelPaths.Count > 0;
        }

        #endregion
    }
}