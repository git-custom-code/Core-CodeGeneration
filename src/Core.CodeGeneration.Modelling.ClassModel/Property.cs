namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    /// <summary>
    /// Represents a single property within an <see cref="IClass"/> that can contain
    /// multiple values.
    /// </summary>
    public sealed class Property : IProperty
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="Property"/> type.
        /// </summary>
        /// <param name="name"> The (unique) name of the property. </param>
        /// <param name="type"> The property's data type. </param>
        public Property(string name, string type)
        {
            Name = name;
            Type = type;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the (unique) name of the property.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the property's data type.
        /// </summary>
        public string Type { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Gets the property's hash code.
        /// </summary>
        /// <returns> The hash code for this instance. </returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <summary>
        /// Converts this instance to a human readable string representation.
        /// </summary>
        /// <returns> A human readable string representation of this instance. </returns>
        public override string ToString()
        {
            return $"{Name}: {Type}";
        }

        #endregion
    }
}