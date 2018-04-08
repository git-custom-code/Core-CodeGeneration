namespace CustomCode.Core.CodeGeneration.Scripting
{
    /// <summary>
    /// Properties within this context can be used directly from the source code of any <see cref="IScript"/>.
    /// </summary>
    public sealed class ScriptContext
    {
        #region Data

        /// <summary>
        /// Gets or sets a collection of script input parameters
        /// </summary>
        /// <example>
        ///
        /// Usage in .csx scripts:
        ///
        /// var param1 = In.Parameter1
        ///
        /// </example>
        public dynamic In { get; }

        #endregion
    }
}