namespace CustomCode.Core.CodeGeneration.Scripting.CodeFiles
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface that defines the fluent api for dynamically creating source code from an <see cref="IScript"/>.
    /// </summary>
    public interface ICodeFile : IFluentInterface
    {
        /// <summary>
        /// Gets the code file's unique id (can be e.g. the path or name of the code file).
        /// </summary>
        Identity<string> Id { get; }

        /// <summary>
        /// Emits the specified <paramref name="sourceCode"/> once.
        /// </summary>
        /// <param name="sourceCode"> The source code to be emitted. </param>
        /// <returns> This <see cref="ICodeFile"/> instance for fluently calling additional api methods. </returns>
        ICodeFile EmitOnce(string sourceCode);

        /// <summary>
        /// Emit the source code created with the <paramref name="createSourceCodeAction"/> once per item in the
        /// specified <paramref name="array"/>.
        /// </summary>
        /// <typeparam name="T"> The type of the specified <paramref name="array"/> items. </typeparam>
        /// <param name="array"> A collection that will create source code for each item. </param>
        /// <param name="createSourceCodeAction">
        /// A delegate that will create source code for each item in the specified <paramref name="array"/>.
        /// </param>
        /// <returns> This <see cref="ICodeFile"/> instance for fluently calling additional api methods. </returns>
        ICodeFile EmitForEach<T>(IEnumerable<T> array, Func<T, string> createSourceCodeAction);

        /// <summary>
        /// Get the code file's generated code.
        /// </summary>
        string GenerateCode();
    }
}