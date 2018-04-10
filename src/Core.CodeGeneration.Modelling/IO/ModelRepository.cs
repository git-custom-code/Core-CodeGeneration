namespace CustomCode.Core.CodeGeneration.Modelling.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Repository that allows access to persisted <see cref="IModel"/> instances.
    /// </summary>
    public sealed class ModelRepository : IModelRepository
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ModelRepository"/> type.
        /// </summary>
        /// <param name="modelFactories">
        /// A collection that contains all registered <see cref="IModelFactory"/> implementations.
        /// </param>
        /// <param name="modelReader">
        /// A <see cref="IModelReader"/> implementation for loading persisted <see cref="IModel"/>s from disk.
        /// </param>
        public ModelRepository(
            IEnumerable<IModelFactory> modelFactories,
            IModelReader modelReader)
        {
            ModelFactories = modelFactories?.ToDictionary(f => f.Id) ?? new Dictionary<Guid, IModelFactory>();
            ModelReader = modelReader;
        }

        /// <summary>
        /// Gets a collection that contains all registered <see cref="IModelFactory"/> implementations.
        /// </summary>
        private IDictionary<Guid, IModelFactory> ModelFactories { get; }

        /// <summary>
        /// Gets a <see cref="IModelReader"/> implementation for loading persisted <see cref="IModel"/>s from disk.
        /// </summary>
        private IModelReader ModelReader { get; }

        #endregion

        #region Data

        /// <summary>
        /// Gets an internal cache for previously parsed <see cref="IModel"/> instances.
        /// </summary>
        private IDictionary<string, IModel> Cache { get; } = new Dictionary<string, IModel>();

        /// <summary>
        /// Gets a lightweight synchronization object for thread safety.
        /// </summary>
        private SemaphoreSlim SyncLock { get; } = new SemaphoreSlim(1, 1);

        #endregion

        #region Logic

        /// <summary>
        /// Load a persisted <see cref="IModel"/> instance from disk.
        /// </summary>
        /// <typeparam name="T"> The type of the model. </typeparam>
        /// <param name="path"> The path to the persisted model file on disk. </param>
        /// <param name="ignoreCache">
        /// True if the internal cache should be ignored, forcing a reload of the model from disk, false otherwise.
        /// </param>
        /// <returns> The persisted <see cref="IModel"/> instance. </returns>
        public async Task<T> LoadAsync<T>(string path, bool ignoreCache = false)
            where T : IModel
        {
            if (ignoreCache)
            {
                var (id, data) = await ModelReader.LoadFromAsync(path);
                if (ModelFactories.TryGetValue(id, out var modelFactory))
                {
                    var model = await modelFactory.CreateAsnyc(data);
                    return (T)model;
                }
            }
            else
            {
                if (Cache.TryGetValue(path, out var model))
                {
                    return (T)model;
                }

                await SyncLock.WaitAsync();
                try
                {
                    if (Cache.TryGetValue(path, out model))
                    {
                        return (T)model;
                    }

                    var (id, data) = await ModelReader.LoadFromAsync(path);
                    if (ModelFactories.TryGetValue(id, out var modelFactory))
                    {
                        model = await modelFactory.CreateAsnyc(data);
                        Cache.Add(path, model);
                        return (T)model;
                    }
                }
                finally
                {
                    SyncLock.Release();
                }
            }

            // ToDo: ExceptionHandling
            return default(T);
        }

        #endregion
    }
}