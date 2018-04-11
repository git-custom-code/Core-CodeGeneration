namespace CustomCode.Core.CodeGeneration.Scripting
{
    using Features;

    /// <summary>
    /// Interface that define's a simple (c#) script.
    /// </summary>
    public interface IScript
    {
        /// <summary>
        /// Gets the script's (c#) source code.
        /// </summary>
        string SourceCode { get; }

        /// <summary>
        /// Access a specific <see cref="IFeature"/> by type.
        /// </summary>
        /// <typeparam name="T"> The type of the feature. </typeparam>
        /// <param name="throwIfFeatureIsMissing"> Toggle if missing features should be ignored or not. </param>
        /// <returns> The requested feature or null (if <paramref name="throwIfFeatureIsMissing"/> is false). </returns>
        /// <exception cref="ExceptionHandling.ScriptMissingFeatureException">
        /// Thrown if the requested feature was missing and <paramref name="throwIfFeatureIsMissing"/> is set to true.
        /// </exception>
        T Feature<T>(bool throwIfFeatureIsMissing = false) where T : IFeature;

        /// <summary>
        /// Query if the script uses a specific <see cref="IFeature"/>.
        /// </summary>
        /// <typeparam name="T"> The type of the feature. </typeparam>
        /// <returns> True if the specified feature is used, false otherwise. </returns>
        bool HasFeature<T>() where T : IFeature;
    }
}