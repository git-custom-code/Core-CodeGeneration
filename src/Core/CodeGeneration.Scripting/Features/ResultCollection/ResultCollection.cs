namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using ExceptionHandling;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Feature for <see cref="IScript"/>s that use result values.
    /// </summary>
    public sealed class ResultCollection : LinkedFeature, IResultCollection, IMutableResultCollection
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ResultCollection"/> type.
        /// </summary>
        /// <param name="resultValueNames"> The (unique) names of the existing script result values. </param>
        public ResultCollection(ISet<string> resultValueNames)
        {
            Values = resultValueNames?.ToDictionary(v => v, v => (object)null) ?? new Dictionary<string, object>();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the script's result values defined by unique name and value.
        /// </summary>
        private Dictionary<string, object> Values { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public IEnumerator<(string name, object value)> GetEnumerator()
        {
            foreach (var value in Values)
            {
                yield return (name: value.Key, value: value.Value);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets a single result value by name.
        /// </summary>
        /// <typeparam name="T"> The type of the result value. </typeparam>
        /// <param name="name"> The (unique) name of the result value. </param>
        /// <returns> The specified result value or an exception if no such value exists. </returns>
        /// <exception cref="ScriptResultNotFoundException">
        /// Thrown if no result value with the specified <paramref name="name"/> was found.
        /// </exception>
        /// <exception cref="ScriptResultTypeCastException">
        /// Thrown if a result value was found but could not be converted to type <typeparamref name="T"/>.
        /// </exception>
        public T GetValue<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ScriptResultNotFoundException(name, Script);
            }

            if (Values.TryGetValue(name, out var result))
            {
                if (result is T convertedResult)
                {
                    return convertedResult;
                }

                throw new ScriptResultTypeCastException(name, typeof(T), result?.GetType(), Script);
            }

            throw new ScriptResultNotFoundException(name, Script);
        }

        /// <summary>
        /// Query if a specific result value exists.
        /// </summary>
        /// <param name="name"> The (unique) name of the result value. </param>
        /// <returns> True if the result value exists, false otherwise. </returns>
        public bool HasValue(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return Values.ContainsKey(name);
        }

        /// <summary>
        /// Converts this instance to a human readable string representation.
        /// </summary>
        /// <returns> A human readable string representation of this instance. </returns>
        public override string ToString()
        {
            return $"Count = {Values.Count}";
        }

        /// <summary>
        /// Try to get a single result value by name.
        /// </summary>
        /// <typeparam name="T"> The type of the result value. </typeparam>
        /// <param name="name"> The (unique) name of the result value. </param>
        /// <param name="value"> The value of the requested result or default if no value exists. </param>
        /// <returns> True if the result value exists, false otherwise. </returns>
        public bool TryGetValue<T>(string name, out T value)
        {
            if (string.IsNullOrEmpty(name))
            {
                value = default(T);
                return false;
            }

            if (Values.TryGetValue(name, out var result))
            {
                if (result is T convertedResult)
                {
                    value = convertedResult;
                    return true;
                }
            }

            value = default(T);
            return false;
        }

        /// <summary>
        /// Update the result values.
        /// </summary>
        /// <param name="outputParameters">
        /// The output values from <see cref="ScriptContext.Out"/> after the <see cref="IScript"/> has been executed.
        /// </param>
        /// <exception cref="ScriptResultMissmatchException">
        /// Thrown if 
        /// </exception>
        public void UpdateValues(dynamic outputParameters)
        {
            if (outputParameters is IDictionary<string, object> parameters)
            {
                foreach (var parameter in parameters)
                {
                    if (Values.ContainsKey(parameter.Key))
                    {
                        Values[parameter.Key] = parameter.Value;
                    }
                }
            }
        }

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
        /// <exception cref="ScriptResultMissmatchException">
        /// Thrown if validation was requested but there was a missmatch between detected and created result values.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the <paramref name="outputParameters"/> is no <see cref="System.Dynamic.ExpandoObject"/>.
        /// </exception>
        public void ValidateResultValueNames(
            dynamic outputParameters,
            bool ignoreAdditionalResultValues = false,
            bool ignoreMissingResultValues = false)
        {
            var parameters = outputParameters as IDictionary<string, object>;
            if (parameters == null)
            {
                throw new ArgumentException("", nameof(outputParameters));
            }

            var additionalResultValues = parameters
                .Where(p => !Values.ContainsKey(p.Key))
                .Select(p => (name: p.Key, value: p.Value))
                .ToList();
            var missingResultValues = Values.Keys.Except(parameters.Keys).ToList();

            if (ignoreAdditionalResultValues == false && additionalResultValues.Count > 0)
            {
                if (ignoreMissingResultValues == false && missingResultValues.Count > 0)
                {
                    throw new ScriptResultMissmatchException(Script, additionalResultValues, missingResultValues);
                }

                throw new ScriptResultMissmatchException(Script, additionalResultValues, missingResultValues);
            }
            else if (ignoreMissingResultValues == false && missingResultValues.Count > 0)
            {
                throw new ScriptResultMissmatchException(Script, additionalResultValues, missingResultValues);
            }
        }

        #endregion
    }
}