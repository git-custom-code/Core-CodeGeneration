namespace CustomCode.Core.CodeGeneration.Scripting
{
    using CustomCode.Core.Composition;
    using Modelling.IO;
    using System.Dynamic;

    /// <summary>
    /// Properties within this context can be used directly from the source code of any <see cref="IScript"/>.
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
        [FactoryConstructor(0)]
        public ScriptContext(dynamic inputParameter, IModelRepository models)
        {
            In = inputParameter; // ?? new ExpandoObject();
            Models = models;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a collection of script input parameters.
        /// </summary>
        /// <example>
        ///
        /// Usage in .csx scripts:
        ///
        /// var param1 = In.Parameter1
        ///
        /// </example>
        public dynamic In { get; }

        /// <summary>
        /// Gets access to persisted <see cref="Modelling.IModel"/> instances.
        /// </summary>
        /// <example>
        ///
        /// Usage in .csx scripts:
        ///
        /// var model = await Models.LoadAsync{IClassModel}(@".\path\to\model.json");
        ///
        /// </example>
        public IModelRepository Models { get; }

        /// <summary>
        /// Gets a collection of script output parameters.
        /// </summary>
        /// <example>
        ///
        /// Usage in .csx scripts:
        ///
        /// Out.Parameter1 = param1;
        ///
        /// </example>
        public dynamic Out { get; } = new ExpandoObject();

        #endregion
    }
}