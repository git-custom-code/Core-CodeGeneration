namespace CustomCode.Core.CodeGeneration.Scripting
{
    using ExceptionHandling;
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

        #region Logic

        /// <summary>
        /// Access a specific <see cref="IFeature"/> by type.
        /// </summary>
        /// <typeparam name="T"> The type of the feature. </typeparam>
        /// <param name="throwIfFeatureIsMissing"> Toggle if missing features should be ignored or not. </param>
        /// <returns> The requested feature or null (if <paramref name="throwIfFeatureIsMissing"/> is false). </returns>
        /// <exception cref="ExceptionHandling.MissingFeatureException"></exception>
        public T Feature<T>(bool throwIfFeatureIsMissing = false) where T : IFeature
        {
            var feature = Features.OfType<T>().FirstOrDefault();
            if (throwIfFeatureIsMissing && feature == null)
            {
                throw new ScriptMissingFeatureException(this, typeof(T));
            }

            return feature;
        }

        /// <summary>
        /// Query if the script uses a specific <see cref="IFeature"/>.
        /// </summary>
        /// <typeparam name="T"> The type of the feature. </typeparam>
        /// <returns> True if the specified feature is used, false otherwise. </returns>
        public bool HasFeature<T>() where T : IFeature
        {
            return Features.OfType<T>().Any();
        }

        #endregion
    }
}