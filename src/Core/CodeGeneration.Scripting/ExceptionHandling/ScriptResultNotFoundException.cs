namespace CustomCode.Core.CodeGeneration.Scripting.ExceptionHandling
{
    /// <summary>
    /// Exception that is raised when a result value of a <see cref="Features.IResultCollection"/> was requested
    /// by name, but no such result value does exist.
    /// </summary>
    public sealed class ScriptResultNotFoundException : ScriptingException
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ScriptResultNotFoundException"/> type.
        /// </summary>
        /// <param name="name"> The name of the result value that could not be found. </param>
        /// <param name="script"> The script that has thrown the exception. </param>
        public ScriptResultNotFoundException(string name, IScript script)
            : base(script, $"A result value with the name <{name}> could not found", "ErrorScriptResultNotFound")
        {
            Name = name;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the name of the result value that could not be found.
        /// </summary>
        public string Name { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Convert exception data to an object array that can be used via <see cref="string.Format(string, object[])"/>
        /// for localization purposes.
        /// </summary>
        /// <returns> The exception's format items for localization or null. </returns>
        public override object[] GetFormatItems()
        {
            return new object[] { Name, Script };
        }

        #endregion
    }
}