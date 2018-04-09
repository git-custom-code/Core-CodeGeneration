namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An <see cref="IFeatureAnalyzer"/> that analyzes if an <see cref="IScript"/> source code
    /// uses input parameters.
    /// </summary>
    public sealed class ParameterCollectionAnalyzer : IFeatureAnalyzer
    {
        #region Data

        /// <summary>
        /// The name of the script's input parameter context.
        /// </summary>
        private const string InputContext = nameof(ScriptContext.In);

        #endregion

        #region Logic

        /// <summary>
        /// Query if the <paramref name="sourceCode"/> uses the <see cref="ParameterCollection"/> feature, i.e. if the
        /// script makes calls to any of the <see cref="ScriptContext.In"/> dynamic input parameters.
        /// </summary>
        /// <param name="sourceCode"> The script's source code (as Roslyn <see cref="SyntaxTree"/> representation). </param>
        /// <param name="feature"> The script's <see cref="ParameterCollection"/> feature (if present) or null otherwise. </param>
        /// <returns> True if the <see cref="ParameterCollection"/> featuree was found, false otherwise. </returns>
        public bool HasFeature(IEnumerable<SyntaxTree> sourceCode, out IFeature feature)
        {
            if (sourceCode == null || !sourceCode.Any())
            {
                feature = null;
                return false;
            }

            var inputParameter = new List<string>();
            foreach (var syntaxTree in sourceCode)
            {
                var root = syntaxTree.GetRoot();
                var memberNodes = root.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                if (memberNodes == null || memberNodes.Count == 0)
                {
                    continue;
                }

                var inputNodes = memberNodes.Where(n => n
                    .DescendantNodes()
                    .OfType<IdentifierNameSyntax>()
                    .Any(name => name.Identifier.Text.Equals(InputContext, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
                if (inputNodes == null || inputNodes.Count == 0)
                {
                    continue;
                }

                var inputNameNodes = inputNodes
                    .SelectMany(n => n
                        .DescendantNodes()
                        .OfType<IdentifierNameSyntax>())
                    .Where(name => !name.Identifier.Text.Equals(InputContext, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (inputNameNodes == null || inputNameNodes.Count == 0)
                {
                    continue;
                }

                foreach (var name in inputNameNodes.Select(n => n.Identifier.Text))
                {
                    inputParameter.Add(name);
                }
            }

            feature = inputParameter.Count == 0 ? null : new ParameterCollection(inputParameter);
            return inputParameter.Count > 0;
        }

        #endregion
    }
}