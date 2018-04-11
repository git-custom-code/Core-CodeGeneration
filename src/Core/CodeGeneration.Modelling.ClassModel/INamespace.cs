namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a single namespace within the <see cref="IClassModel"/> that can contain
    /// multiple <see cref="IClass"/>es.
    /// </summary>
    [JsonConverter(typeof(NamespaceConverter))]
    public interface INamespace
    {
        /// <summary>
        /// Gets a class by <paramref name="name"/>.
        /// </summary>
        /// <param name="name"> The class' name. </param>
        /// <returns> The requested class or null. </returns>
        IClass this[string name] { get; }

        /// <summary>
        /// Gets the (unique) name of the namespace.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a collection of all classes within the namespace.
        /// </summary>
        ISet<IClass> Classes { get; }
    }
}