namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    /// <summary>
    /// Internal implementation detail that allows <see cref="IFeature"/>s to be linked to an <see cref="IScript"/>.
    /// </summary>
    internal interface ILinkedFeature
    {
        /// <summary>
        /// Link a feature to the specified <paramref name="script"/>.
        /// </summary>
        /// <param name="script"> The script that should be associated with the feature. </param>
        void LinkWith(IScript script);
    }
}