namespace CustomCode.Core.CodeGeneration.Scripting
{
    using Features;
    using Modelling.IO;

    /// <summary>
    /// Context properties can be used directly from any <see cref="IScript"/>'s source code.
    /// </summary>
    public interface IScriptContext
    {
        /// <summary>
        /// Gets access to the collection of generated code files and the Emit APIs.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// Usage in .csx scripts:
        /// 
        /// Code.CreateOrGetFile("Foo.cs")
        ///     .EmitOnce(@"
        ///         public sealed class Foo
        ///         {{
        ///             public Foo()
        ///             {{ }}
        ///
        ///             public IBar Bar {{ get; }}
        ///         }}");
        /// ]]>
        /// </example>
        ICodeFileCollection Code { get; }

        /// <summary>
        /// Gets a collection of script input parameters.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// Usage in .csx scripts:
        /// 
        /// var value1 = In.Parameter1
        /// ]]>
        /// </example>
        dynamic In { get; }

        /// <summary>
        /// Gets access to persisted <see cref="Modelling.IModel"/> instances.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// Usage in .csx scripts:
        ///
        /// var model = await Models.LoadAsync<IClassModel>(@".\path\to\model.json");
        /// ]]>
        /// </example>
        IModelRepository Models { get; }

        /// <summary>
        /// Gets a collection of script output parameters.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// Usage in .csx scripts:
        ///
        /// Out.Parameter1 = value1;
        /// ]]>
        /// </example>
        dynamic Out { get; }
    }
}