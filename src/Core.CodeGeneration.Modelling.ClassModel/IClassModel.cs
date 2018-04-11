namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// An <see cref="IModel"/> implementation for modelling c# classes.
    /// </summary>
    [JsonConverter(typeof(ClassModelConverter))]
    public interface IClassModel : IModel
    {
        /// <summary>
        /// Gets a namespace by <paramref name="name"/>.
        /// </summary>
        /// <param name="name"> The namespace's name. </param>
        /// <returns> The requested namespace or null. </returns>
        INamespace this[string name] { get; }

        /// <summary>
        /// Gets a collection of all namespaces within the domain.
        /// </summary>
        ISet<INamespace> Namespaces { get; }
    }
}