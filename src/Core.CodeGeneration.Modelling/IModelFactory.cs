namespace CustomCode.Core.CodeGeneration.Modelling
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for a factory that can create <see cref="IModel"/> instances.
    /// </summary>
    public interface IModelFactory
    {
        /// <summary>
        /// Gets the factory's unique id.
        /// </summary>
        /// <remarks>
        /// Note that this id is persisted inside of .model files and is used to identify the correct
        /// <see cref="IModelFactory"/> for loading the persisted <see cref="IModel"/> from disk.
        /// </remarks>
        Guid Id { get; }

        /// <summary>
        /// Create a model instance from ther model's persisted data.
        /// </summary>
        /// <param name="data"> The persisted model data. </param>
        /// <returns> An <see cref="IModel"/> instance that represents the persisted data. </returns>
        Task<IModel> CreateAsnyc(string data);
    }
}