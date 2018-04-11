namespace CustomCode.Core.CodeGeneration.Scripting.ExceptionHandling
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Exception that is raised when a script uses parameters that are not supplied by the caller.
    /// </summary>
    public sealed class ScriptParameterMissmatchException : ScriptingException
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ScriptParameterMissmatchException"/> type.
        /// </summary>
        /// <param name="script"> The script that has thrown the exception. </param>
        /// <param name="additionalScriptParameters">
        /// A collection of input parameters that were supplied but are not used by the script.
        /// </param>
        /// <param name="missingScriptParameters">
        /// A collection of input parameters that are used by the script but where not supplied by the caller.
        /// </param>
        public ScriptParameterMissmatchException(
            IScript script,
            IEnumerable<(string name, object value)> additionalScriptParameters,
            IEnumerable<string> missingScriptParameters)
            : base(script, "A missmatch between supplied and used script parameters has occured.", "ErrorScriptParameterMissmatch")
        {
            AdditionalScriptParameters = additionalScriptParameters ?? Enumerable.Empty<(string name, object value)>();
            MissingScriptParameters = missingScriptParameters ?? Enumerable.Empty<string>();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a collection of input parameters that were supplied but are not used by the script.
        /// </summary>
        public IEnumerable<(string name, object value)> AdditionalScriptParameters { get; }

        /// <summary>
        /// Gets a collection of input parameters that are used by the script but where not supplied by the caller.
        /// </summary>
        public IEnumerable<string> MissingScriptParameters { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Convert exception data to an object array that can be used via <see cref="string.Format(string, object[])"/>
        /// for localization purposes.
        /// </summary>
        /// <returns> The exception's format items for localization or null. </returns>
        public override object[] GetFormatItems()
        {
            return new object[] { Script, AdditionalScriptParameters, MissingScriptParameters };
        }

        #endregion
    }
}