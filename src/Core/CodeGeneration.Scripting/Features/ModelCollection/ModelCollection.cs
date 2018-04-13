namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Feature for <see cref="IScript"/>s that use <see cref="Modelling.IModel"/> instances.
    /// </summary>
    public sealed class ModelCollection : LinkedFeature, IModelCollection
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ModelCollection"/> type.
        /// </summary>
        /// <param name="modelPaths"> A collection with the used model paths. </param>
        public ModelCollection(IEnumerable<string> modelPaths)
        {
            Models = modelPaths?.Distinct()?.ToDictionary(path => Path.GetFileNameWithoutExtension(path), path => path) ?? new Dictionary<string, string>();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the script's models.
        /// </summary>
        private IDictionary<string, string> Models { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public IEnumerator<(string name, string path)> GetEnumerator()
        {
            foreach (var model in Models)
            {
                yield return (model.Key, model.Value);
            }
        }

        #endregion
    }
}