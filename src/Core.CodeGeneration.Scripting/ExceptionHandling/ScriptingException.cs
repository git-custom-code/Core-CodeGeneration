namespace CustomCode.Core.CodeGeneration.Scripting.ExceptionHandling
{
    using Core.ExceptionHandling;
    using System;

    /// <summary>
    /// Base exception type for all exceptions related to scripting.
    /// </summary>
    /// <remarks>
    /// Since scripts are intended to be created by developers and there is usually nothing an application user
    /// can do against scripting errors, scripting exceptions are considered to be <see cref="TechnicalException"/>s.
    /// </remarks>
    public abstract class ScriptingException : TechnicalException
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ScriptingException"/> type.
        /// </summary>
        /// <param name="script"> The script that has thrown the exception. </param>
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
        protected ScriptingException(
            IScript script,
            string developerMessage = "An unexcepted scripting exception has occured.",
            string userMessageResourceKey = "ErrorScriptingDefaultMessage")
            : base(developerMessage, userMessageResourceKey)
        {
            Script = script;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ScriptingException"/> type.
        /// </summary>
        /// <param name="script"> The script that has thrown the exception. </param>
        /// <param name="innerException"> The exception that caused this exception to be thrown. </param>
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
        protected ScriptingException(
            IScript script,
            Exception innerException,
            string developerMessage = "An unexcepted scripting exception has occured.",
            string userMessageResourceKey = "ErrorScriptingDefaultMessage")
            : base(innerException, developerMessage, userMessageResourceKey)
        {
            Script = script;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the script that has thrown the exception.
        /// </summary>
        public IScript Script { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Convert exception data to an object array that can be used via <see cref="string.Format(string, object[])"/>
        //  for localization purposes.
        /// </summary>
        /// <returns> The exception's format items for localization or null. </returns>
        public override object[] GetFormatItems()
        {
            return new object[] { Script };
        }

        #endregion
    }
}