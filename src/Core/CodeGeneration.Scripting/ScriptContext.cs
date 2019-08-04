namespace CustomCode.Core.CodeGeneration.Scripting
{
    using Features;
    using Composition;
    using Modelling.IO;
    using System.Dynamic;

    /// <summary>
    /// Context properties can be used directly from any <see cref="IScript"/>'s source code.
    /// </summary>
    [Export]
    public sealed class ScriptContext : IScriptContext
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ScriptContext"/> type.
        /// </summary>
        /// <param name="inputParameter"> The context's input parameters. </param>
        /// <param name="models"> Repository that allows access to persisted <see cref="Modelling.IModel"/> instances. </param>
        /// <param name="codeFileCollection"> The script's generated C# code files. </param>
        [FactoryParameters("inputParameter")]
        public ScriptContext(dynamic inputParameter, IModelRepository models, ICodeFileCollection codeFileCollection)
        {
            Code = codeFileCollection;
            In = inputParameter ?? new ExpandoObject();
            Models = models;
        }

        #endregion

        #region Data

        /// <inheritdoc />
        public ICodeFileCollection Code { get; }

        /// <inheritdoc />
        public dynamic In { get; }

        /// <inheritdoc />
        public IModelRepository Models { get; }

        /// <inheritdoc />
        public dynamic Out { get; } = new ExpandoObject();

        #endregion
    }
}