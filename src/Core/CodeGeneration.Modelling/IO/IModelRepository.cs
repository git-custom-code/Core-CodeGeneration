namespace CustomCode.Core.CodeGeneration.Modelling.IO
{
    using System.Threading.Tasks;

    /// <summary>
    /// Repository that allows access to persisted <see cref="IModel"/> instances.
    /// </summary>
    public interface IModelRepository
    {
        /// <summary>
        /// Load a persisted <see cref="IModel"/> instance from disk.
        /// </summary>
        /// <typeparam name="T"> The type of the model. </typeparam>
        /// <param name="path"> The path to the persisted model file on disk. </param>
        /// <param name="ignoreCache">
        /// True if the internal cache should be ignored, forcing a reload of the model from disk, false otherwise.
        /// </param>
        /// <returns> The persisted <see cref="IModel"/> instance. </returns>
        Task<T> LoadAsync<T>(string path, bool ignoreCache = false) where T : IModel;
    }
}