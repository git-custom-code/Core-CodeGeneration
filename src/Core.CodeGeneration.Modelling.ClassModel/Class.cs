namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a single class within an <see cref="INamespace"/> that can contain
    /// multiple <see cref="IProperty"/>s.
    /// </summary>
    public sealed class Class : IClass
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="Class"/> type.
        /// </summary>
        /// <param name="name"> The (unique) name of the class. </param>
        /// <param name="properties"> A collection of all properties within the class. </param>
        public Class(string name, ISet<IProperty> properties)
        {
            Name = name ?? string.Empty;
            Properties = properties ?? new HashSet<IProperty>();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a property by <paramref name="name"/>.
        /// </summary>
        /// <param name="name"> The property's name. </param>
        /// <returns> The requested property or null. </returns>
        public IProperty this[string name]
        {
            get { return Properties.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)); }
        }

        /// <summary>
        /// Gets a collection of all properties within the class.
        /// </summary>
        public ISet<IProperty> Properties { get; }

        /// <summary>
        /// Gets the (unique) name of the class.
        /// </summary>
        public string Name { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Gets the class' hash code.
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
            var count = Properties.Count();
            if (count == 1)
            {
                return $"{Name}: 1 property";
            }
            return $"{Name}: {count} properties";
        }

        #endregion
    }
}