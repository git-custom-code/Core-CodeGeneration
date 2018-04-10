namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    /// <summary>
    /// Interface for the implementation detail, that the <see cref="IScriptRunner"/> needs to update
    /// the result values after the <see cref="IScript"/> has been executed.
    /// </summary>
    internal interface IMutableResultCollection : IFeature
    {
        /// <summary>
        /// Update the result values.
        /// </summary>
        /// <param name="outputParameters">
        /// The output values from <see cref="ScriptContext.Out"/> after the <see cref="IScript"/> has been executed.
        /// </param>
        void UpdateValues(dynamic outputParameters);

        /// <summary>
        /// Validate if detected script result values match with the result values created by the script.
        /// </summary>
        /// <param name="outputParameters"> A collection of created script result values. </param>
        /// <param name="ignoreAdditionalResultValues">
        /// True if additional result values (created by the script but not detected by the analyzer)
        /// should not be validated, false otherwise.
        /// </param>
        /// <param name="ignoreMissingResultValues">
        /// True if missing result values (detected by the analyzer but not created by the script)
        /// should not be validated, false otherwise.
        /// </param>
        /// <exception cref="ExceptionHandling.ScriptResultMissmatchException">
        /// Thrown if validation was requested but there was a missmatch between detected and created result values.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown if the <paramref name="outputParameters"/> is no <see cref="System.Dynamic.ExpandoObject"/>.
        /// </exception>
        void ValidateResultValueNames(
            dynamic outputParameters,
            bool ignoreAdditionalResultValues = false,
            bool ignoreMissingResultValues = false);
    }
}