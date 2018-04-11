namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    using CustomCode.Core.Composition;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// <see cref="IModelFactory"/> implementation for creating <see cref="IClassModel"/> instances.
    /// </summary>
    [Export]
    [Export(typeof(IModelFactory), ServiceName = nameof(ClassModelFactory))]
    public sealed class ClassModelFactory : IModelFactory
    {
        #region Data

        /// <summary>
        /// The unique id used to identify <see cref="IClassModel"/> files.
        /// </summary>
        public const string TypeId = "5bc5bb1f-60eb-419e-9458-c11f9a494001";

        /// <summary>
        /// Gets the factory's unique id.
        /// </summary>
        public Guid Id { get; } = new Guid(TypeId);

        #endregion

        #region Logic

        /// <summary>
        /// Create a model instance from ther model's persisted data.
        /// </summary>
        /// <param name="data"> The persisted model data. </param>
        /// <returns> An <see cref="IModel"/> instance that represents the persisted data. </returns>
        public Task<IModel> CreateAsnyc(string data)
        {
            return Task.Run<IModel>(() => JsonConvert.DeserializeObject<IClassModel>(data));
        }

        #endregion
    }
}