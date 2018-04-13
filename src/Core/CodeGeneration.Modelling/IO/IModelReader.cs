using System;
namespace CustomCode.Core.CodeGeneration.Modelling.IO
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that allows asynchronous loading of a persisted <see cref="IModel"/> from disk.
    /// </summary>
    public interface IModelReader
    {
        /// <summary>
        /// Asynchronously load a persisted <see cref="IModel"/> from disk.
        /// </summary>
        /// <param name="path"> The path to the persisted model. </param>
        /// <returns> The model's (type) id and content. </returns>
        Task<(Guid id, string data)> LoadFromAsync(string path);
    }
}