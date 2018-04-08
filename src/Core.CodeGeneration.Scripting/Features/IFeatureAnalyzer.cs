namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using Microsoft.CodeAnalysis;
    using System.Collections.Generic;

    /// <summary>
    /// Interface that can be used to analyze the source code of an <see cref="IScript"/> and
    /// detect if a specific <see cref="IFeature"/> is used (by the script's code).
    /// </summary>
    public interface IFeatureAnalyzer
    {
        /// <summary>
        /// Query if a specific <paramref name="feature"/> is present in the source code of an <see cref="IScript"/>.
        /// </summary>
        /// <param name="sourceCode"> The script's source code (as Roslyn <see cref="SyntaxTree"/> representation). </param>
        /// <param name="feature"> The script's <see cref="IFeature"/> (if present) or null otherwise. </param>
        /// <returns> True if the script's <see cref="IFeature"/> was found, false otherwise. </returns>
        bool HasFeature(IEnumerable<SyntaxTree> sourceCode, out IFeature feature);
    }
}