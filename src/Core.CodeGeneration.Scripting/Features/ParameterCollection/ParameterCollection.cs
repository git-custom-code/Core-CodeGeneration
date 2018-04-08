namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;

    /// <summary>
    /// Feature for <see cref="IScript"/>s that use input parameters.
    /// </summary>
    public sealed class ParameterCollection : LinkedFeature, IParameterCollection
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ParameterCollection"/> type.
        /// </summary>
        /// <param name="parameterNames"> A collection with the used script parameter names (without values). </param>
        public ParameterCollection(IEnumerable<string> parameterNames)
        {
            Parameters = parameterNames?.Distinct()?.ToDictionary(name => name, name => (object)null) ?? new Dictionary<string, object>();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the script's input parameters as dynamic object.
        /// </summary>
        private dynamic DynamicParameter { get; set; }

        /// <summary>
        /// Gets the script's input parameters.
        /// </summary>
        private IDictionary<string, object> Parameters { get; }

        /// <summary>
        /// Gets a lightweight synchronization object.
        /// </summary>
        private object SyncLock { get; } = new object();

        #endregion

        #region Logic

        /// <summary>
        /// Gets the script's input parameters as dynamic type for conveniant usage inside of a script file.
        /// </summary>
        public dynamic AsDynamic()
        {
            if (DynamicParameter == null)
            {
                lock (SyncLock)
                {
                    if (DynamicParameter == null)
                    {
                        DynamicParameter = ConvertToDynamic(Parameters);
                    }
                }
            }

            return DynamicParameter;
        }

        /// <summary>
        /// Convert an object to a dynamic (expando object) type.
        /// This allows a conveniant usage of properties inside of script files.
        /// </summary>
        /// <param name="input"> The input object that should be converted to dynamic. </param>
        /// <returns> A dynamic representation of the <paramref name="input"/> object's data. </returns>
        private dynamic ConvertToDynamic(IDictionary<string, object> input)
        {
            if (input == null)
            {
                return new ExpandoObject();
            }

            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var tupel in input)
            {
                expando.Add(tupel.Key, tupel.Value);
            }

            return expando;
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
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate through the collection. </returns>
        public IEnumerator<(string name, object value)> GetEnumerator()
        {
            foreach (var parameter in Parameters)
            {
                yield return (parameter.Key, parameter.Value);
            }
        }

        /// <summary>
        /// Update the script's parameter values.
        /// </summary>
        /// <param name="parameters"> The values to be updated. </param>
        public void UpdateValues(params (string name, object value)[] parameters)
        {
            foreach (var (name, value) in parameters)
            {
                if (Parameters.ContainsKey(name))
                {
                    Parameters[name] = value;
                }
                else
                {
                    // ToDo
                }
            }
        }

        #endregion
    }
}