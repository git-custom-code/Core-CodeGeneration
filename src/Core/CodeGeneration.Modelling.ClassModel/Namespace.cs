namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a single namespace within the <see cref="IClassModel"/> that can contain
    /// multiple <see cref="IClass"/>es.
    /// </summary>
    public sealed class Namespace : INamespace
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="Namespace"/> type.
        /// </summary>
        /// <param name="name"> The (unique) name of the namespace. </param>
        /// <param name="classes"> A collection of all classes within the namespace. </param>
        public Namespace(string name, ISet<IClass> classes)
        {
            Name = name ?? string.Empty;
            Classes = classes ?? new HashSet<IClass>();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a class by <paramref name="name"/>.
        /// </summary>
        /// <param name="name"> The class' name. </param>
        /// <returns> The requested class or null. </returns>
        public IClass this[string name]
        {
            get { return Classes.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)); }
        }

        /// <summary>
        /// Gets a collection of all classes within the namespace.
        /// </summary>
        public ISet<IClass> Classes { get; }

        /// <summary>
        /// Gets the (unique) name of the namespace.
        /// </summary>
        public string Name { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Gets the namespace's hash code.
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
            var count = Classes.Count();
            if (count == 1)
            {
                return $"{Name}: 1 class";
            }

            return $"{Name}: {count} classes";
        }

        #endregion
    }
}