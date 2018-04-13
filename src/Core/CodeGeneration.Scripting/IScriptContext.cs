namespace CustomCode.Core.CodeGeneration.Scripting
{
    using Modelling.IO;

    /// <summary>
    /// Properties within this context can be used directly from the source code of any <see cref="IScript"/>.
    /// </summary>
    public interface IScriptContext
    {
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
        dynamic In { get; }

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
        IModelRepository Models { get; }

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
        dynamic Out { get; }
    }
}