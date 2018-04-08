namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    /// <summary>
    /// Abstract base type for <see cref="IFeature"/> implementations that overrides
    /// equality operators and logic to ensure that features are unique within a single <see cref="IScript"/>,
    /// i.e. each feature type can only exist once.
    /// </summary>
    public abstract class LinkedFeature : IFeature, ILinkedFeature
    {
        #region Data

        /// <summary>
        /// Gets the feature's associated script.
        /// </summary>
        public IScript Script { get; private set; }

        #endregion

        #region Logic

        /// <summary>
        /// Compares two features for equality.
        /// </summary>
        /// <param name="left"> The operator's left hand side parameter. </param>
        /// <param name="right"> The operator's right hand side parameter. </param>
        /// <returns> True if both features are of the same type, false otherwise. </returns>
        public static bool operator ==(LinkedFeature left, LinkedFeature right)
        {
            var leftTypeName = left?.GetType()?.Name;
            var rightTypeName = right?.GetType()?.Name;
            return leftTypeName == rightTypeName;
        }

        /// <summary>
        /// Compares two features for inequality.
        /// </summary>
        /// <param name="left"> The operator's left hand side parameter. </param>
        /// <param name="right"> The operator's right hand side parameter. </param>
        /// <returns> False if both features are of the same type, true otherwise. </returns>
        public static bool operator !=(LinkedFeature left, LinkedFeature right)
        {
            var leftTypeName = left?.GetType()?.Name;
            var rightTypeName = right?.GetType()?.Name;
            return leftTypeName != rightTypeName;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="obj"/> is equal to the current feature.
        /// </summary>
        /// <param name="obj"> The object to compare with the current feature. </param>
        /// <returns> True if the specified object is equal to the current feature, false otherwise. </returns>
        public override bool Equals(object obj)
        {
            if (obj is LinkedFeature other)
            {
                var otherTypeName = other?.GetType()?.Name;
                var typeName = GetType().Name;
                return otherTypeName != typeName;
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code for this <see cref="IScriptFeature"/>.
        /// </summary>
        /// <returns> A 32-bit signed integer hash code. </returns>
        public override int GetHashCode()
        {
            return GetType().Name.GetHashCode();
        }

        /// <summary>
        /// Link a feature to the specified <paramref name="script"/>.
        /// </summary>
        /// <param name="script"> The script that should be associated with the feature. </param>
        public void LinkWith(IScript script)
        {
            Script = script;
        }

        #endregion
    }
}