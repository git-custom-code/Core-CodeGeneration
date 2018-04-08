namespace CustomCode.Core.CodeGeneration.Scripting
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that define's a simple (c#) script.
    /// </summary>
    public interface IScript
    {
        /// <summary>
        /// Gets the script's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the script's full path.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Gets the script's (c#) code asynchronously.
        /// </summary>
        /// <returns> The script's (c#) code. </returns>
        Task<string> GetCodeAsync();
    }
}