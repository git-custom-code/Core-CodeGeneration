namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a single class within an <see cref="INamespace"/> that can contain
    /// multiple <see cref="IProperty"/>s.
    /// </summary>
    [JsonConverter(typeof(ClassConverter))]
    public interface IClass
    {
        /// <summary>
        /// Gets a property by <paramref name="name"/>.
        /// </summary>
        /// <param name="name"> The property's name. </param>
        /// <returns> The requested property or null. </returns>
        IProperty this[string name] { get; }

        /// <summary>
        /// Gets the (unique) name of the class.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a collection of all properties within the class.
        /// </summary>
        ISet<IProperty> Properties { get; }
    }
}