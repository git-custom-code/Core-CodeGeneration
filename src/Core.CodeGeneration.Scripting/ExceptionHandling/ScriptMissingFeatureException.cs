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
        /// Creates a new instance of the <see cref="ParameterMissmatchException"/> type.
        /// </summary>
        /// <param name="script"> The script that has thrown the exception. </param>
        /// <param name="featureType"> The type of the missing script feature. </param>
        /// <param name="developerMessage">
        /// The exception's message.
        /// Note that this message is not localized and should not be displayed to the application users.
        /// This message is meant to contain developer relevant informations and can be used for logging purposes.
        /// </param>
        /// <param name="userMessageResourceKey">
        /// A .resx key that can be used to localize the exception's message.
        /// Note that this message should be displayed to the application users and therefore can or should be localized
        /// if the application requires it. This message should not contain any informations that is irrelevant
        /// or confusing for the application users.
        /// </param>
        public ScriptMissingFeatureException(
            IScript script,
            Type featureType,
            string developerMessage = "A script feature is missing.",
            string userMessageResourceKey = "ErrorScriptParameterMissmatch")
            : base(script, developerMessage, userMessageResourceKey)
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
        //  for localization purposes.
        /// </summary>
        /// <returns> The exception's format items for localization or null. </returns>
        public override object[] GetFormatItems()
        {
            return new object[] { Script, FeatureType };
        }

        #endregion
    }
}