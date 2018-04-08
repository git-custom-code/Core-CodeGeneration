namespace CustomCode.Core.CodeGeneration.Scripting
{
    using System.Dynamic;

    /// <summary>
    /// Properties within this context can be used directly from the source code of any <see cref="IScript"/>.
    /// </summary>
    public sealed class ScriptContext
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ScriptContext"/> type.
        /// </summary>
        public ScriptContext()
        {
            In = new ExpandoObject();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ScriptContext"/> type.
        /// </summary>
        /// <param name="inputParameter"> The context's input parameters. </param>
        public ScriptContext(dynamic inputParameter)
        {
            In = inputParameter;
        }

        #endregion

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