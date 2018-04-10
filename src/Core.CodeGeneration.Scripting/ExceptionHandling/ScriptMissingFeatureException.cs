namespace CustomCode.Core.CodeGeneration.Scripting.ExceptionHandling
{
    using System;

    /// <summary>
    /// Exception that is raised when an invalid script feature is requested.
    /// </summary>
    public sealed class ScriptMissingFeatureException : ScriptingException
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ScriptMissingFeatureException"/> type.
        /// </summary>
        /// <param name="script"> The script that has thrown the exception. </param>
        /// <param name="featureType"> The type of the missing script feature. </param>
        public ScriptMissingFeatureException(IScript script, Type featureType)
            : base(script, $"The feature <{featureType.Name} is missing.", "ErrorScriptParameterMissmatch")
        {
            FeatureType = featureType;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the type of the missing feature.
        /// </summary>
        public Type FeatureType { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Convert exception data to an object array that can be used via <see cref="string.Format(string, object[])"/>
        ///  for localization purposes.
        /// </summary>
        /// <returns> The exception's format items for localization or null. </returns>
        public override object[] GetFormatItems()
        {
            return new object[] { Script, FeatureType };
        }

        #endregion
    }
}