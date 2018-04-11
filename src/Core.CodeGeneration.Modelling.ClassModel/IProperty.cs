namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    /// <summary>
    /// Represents a single property within an <see cref="IClass"/> that can contain
    /// multiple values.
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// Gets the (unique) name of the property.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the property's data type.
        /// </summary>
        string Type { get; }
    }
}