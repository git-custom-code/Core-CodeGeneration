namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using System.Collections.Generic;

    /// <summary>
    /// Feature for <see cref="IScript"/>s that use result values.
    /// </summary>
    public interface IResultCollection : IFeature, IEnumerable<(string name, object value)>
    {
        /// <summary>
        /// Gets a single result value by name.
        /// </summary>
        /// <typeparam name="T"> The type of the result value. </typeparam>
        /// <param name="name"> The (unique) name of the result value. </param>
        /// <returns> The specified result value or an exception if no such value exists. </returns>
        /// <exception cref="ExceptionHandling.ScriptResultNotFoundException">
        /// Thrown if no result value with the specified <paramref name="name"/> was found.
        /// </exception>
        /// <exception cref="ExceptionHandling.ScriptResultTypeCastException">
        /// Thrown if a result value was found but could not be converted to type <typeparamref name="T"/>.
        /// </exception>
        T GetValue<T>(string name);

        /// <summary>
        /// Query if a specific result value exists.
        /// </summary>
        /// <param name="name"> The (unique) name of the result value. </param>
        /// <returns> True if the result value exists, false otherwise. </returns>
        bool HasValue(string name);

        /// <summary>
        /// Try to get a single result value by name.
        /// </summary>
        /// <typeparam name="T"> The type of the result value. </typeparam>
        /// <param name="name"> The (unique) name of the result value. </param>
        /// <param name="value"> The value of the requested result or default if no value exists. </param>
        /// <returns> True if the result value exists, false otherwise. </returns>
        bool TryGetValue<T>(string name, out T value);
    }
}