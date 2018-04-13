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
    /// uses result values.
    /// </summary>
    [Export]
    public sealed class ResultCollectionAnalyzer : IFeatureAnalyzer
    {
        #region Data

        /// <summary>
        /// The name of the script's result values context.
        /// </summary>
        private const string OutputContext = nameof(ScriptContext.Out);

        #endregion

        #region Logic

        /// <summary>
        /// Query if the <paramref name="sourceCode"/> uses the <see cref="ParameterCollection"/> feature, i.e. if the
        /// script makes calls to any of the <see cref="ScriptContext.Out"/> dynamic result values.
        /// </summary>
        /// <param name="sourceCode"> The script's source code (as Roslyn <see cref="SyntaxTree"/> representation). </param>
        /// <param name="feature"> The script's <see cref="ResultCollection"/> feature (if present) or null otherwise. </param>
        /// <returns> True if the <see cref="ResultCollection"/> feature was found, false otherwise. </returns>
        public bool HasFeature(IEnumerable<SyntaxTree> sourceCode, out IFeature feature)
        {
            if (sourceCode == null || !sourceCode.Any())
            {
                feature = null;
                return false;
            }

            var outputParameter = new HashSet<string>();
            foreach (var syntaxTree in sourceCode)
            {
                var root = syntaxTree.GetRoot();
                var memberNodes = root.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                if (memberNodes == null || memberNodes.Count == 0)
                {
                    continue;
                }

                var outputNodes = memberNodes.Where(n => n
                    .DescendantNodes()
                    .OfType<IdentifierNameSyntax>()
                    .Any(name => name.Identifier.Text.Equals(OutputContext, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
                if (outputNodes == null || outputNodes.Count == 0)
                {
                    continue;
                }

                var outputNameNodes = outputNodes
                    .SelectMany(n => n
                        .DescendantNodes()
                        .OfType<IdentifierNameSyntax>())
                    .Where(name => !name.Identifier.Text.Equals(OutputContext, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (outputNameNodes == null || outputNameNodes.Count == 0)
                {
                    continue;
                }

                foreach (var name in outputNameNodes.Select(n => n.Identifier.Text).Distinct())
                {
                    outputParameter.Add(name);
                }
            }

            feature = outputParameter.Count == 0 ? null : new ResultCollection(outputParameter);
            return outputParameter.Count > 0;
        }

        #endregion
    }
}