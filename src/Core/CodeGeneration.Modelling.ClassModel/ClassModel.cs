namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An <see cref="IModel"/> implementation for modelling c# classes.
    /// </summary>
    public sealed class ClassModel : IClassModel
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ClassModel"/> type.
        /// </summary>
        /// <param name="namespaces"> A collection of all namespaces within the domain. </param>
        public ClassModel(ISet<INamespace> namespaces)
        {
            Namespaces = namespaces ?? new HashSet<INamespace>();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a collection of all namespaces within the domain.
        /// </summary>
        public ISet<INamespace> Namespaces { get; }

        /// <summary>
        /// Gets a namespace by <paramref name="name"/>.
        /// </summary>
        /// <param name="name"> The namespace's name. </param>
        /// <returns> The requested namespace or null. </returns>
        public INamespace this[string name]
        {
            get { return Namespaces.FirstOrDefault(n => string.Equals(n.Name, name, StringComparison.OrdinalIgnoreCase)); }
        }

        #endregion

        #region Logic

        /// <summary>
        /// Converts this instance to a human readable string representation.
        /// </summary>
        /// <returns> A human readable string representation of this instance. </returns>
        public override string ToString()
        {
            var count = Namespaces.Count();
            if (count == 1)
            {
                return "1 namespace";
            }
            return $"{count} namespaces";
        }

        #endregion
    }
}