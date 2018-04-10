namespace CustomCode.Core.CodeGeneration.Scripting.ExceptionHandling
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Exception that is raised when a script uses result values that could not be detected.
    /// </summary>
    public sealed class ScriptResultMissmatchException : ScriptingException
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ScriptResultMissmatchException"/> type.
        /// </summary>
        /// <param name="script"> The script that has thrown the exception. </param>
        /// <param name="additionalScriptResults">
        /// A collection of result values that were created by the script but are not detected by the analyzer.
        /// </param>
        /// <param name="missingScriptResults">
        /// Acollection of result values that are detected by the analyzer but not created by the script.
        /// </param>
        public ScriptResultMissmatchException(
            IScript script,
            IEnumerable<(string name, object value)> additionalScriptResults,
            IEnumerable<string> missingScriptResults)
            : base(script, "A missmatch between detected and created script result values has occured.", "ErrorScriptResultMissmatch")
        {
            AdditionalScriptResults = additionalScriptResults ?? Enumerable.Empty<(string name, object value)>();
            MissingScriptResults = missingScriptResults ?? Enumerable.Empty<string>();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a collection of result values that were created by the script but are not detected by the analyzer.
        /// </summary>
        public IEnumerable<(string name, object value)> AdditionalScriptResults { get; }

        /// <summary>
        /// Gets a collection of result values that are detected by the analyzer but not created by the script.
        /// </summary>
        public IEnumerable<string> MissingScriptResults { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Convert exception data to an object array that can be used via <see cref="string.Format(string, object[])"/>
        /// for localization purposes.
        /// </summary>
        /// <returns> The exception's format items for localization or null. </returns>
        public override object[] GetFormatItems()
        {
            return new object[] { Script, AdditionalScriptResults, MissingScriptResults };
        }

        #endregion
    }
}