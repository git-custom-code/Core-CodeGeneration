namespace CustomCode.Core.CodeGeneration.Scripting
{
    using Extensions;
    using Features;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Type that represents a script that contains (c#) source code.
    /// </summary>
    public class Script : IScript
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="Script"/> type.
        /// </summary>
        /// <param name="sourceCode"> The script file's source code. </param>
        /// <param name="features"> A set of unique script features (e.g. input parameters, result values, ...). </param>
        public Script(string sourceCode, ISet<IFeature> features)
        {
            Features = features ?? new HashSet<IFeature>();
            Features.OfType<ILinkedFeature>().Apply(feature => feature.LinkWith(this));
            SourceCode = sourceCode;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a set of unique script features.
        /// </summary>
        private ISet<IFeature> Features { get; }

        /// <summary>
        /// Gets the script's (c#) source code.
        /// </summary>
        public string SourceCode { get; }

        #endregion
    }
}