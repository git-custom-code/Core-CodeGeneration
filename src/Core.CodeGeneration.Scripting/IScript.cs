namespace CustomCode.Core.CodeGeneration.Scripting
{
     /// <summary>
    /// Interface that define's a simple (c#) script.
    /// </summary>
    public interface IScript
    {
        /// <summary>
        /// Gets the script's (c#) source code.
        /// </summary>
        string SourceCode { get; }
    }
}