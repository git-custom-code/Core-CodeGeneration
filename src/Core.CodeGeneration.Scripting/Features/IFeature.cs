namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    /// <summary>
    /// Common interface for all <see cref="IScript"/> features.
    /// </summary>
    public interface IFeature
    {
        /// <summary>
        /// Gets the feature's associated script.
        /// </summary>
        IScript Script { get; }
    }
}