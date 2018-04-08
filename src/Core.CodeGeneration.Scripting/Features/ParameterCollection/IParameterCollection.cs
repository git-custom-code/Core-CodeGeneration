namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using System.Collections.Generic;

    /// <summary>
    /// Feature for <see cref="IScript"/>s that use input parameters.
    /// </summary>
    public interface IParameterCollection : IFeature, IEnumerable<(string name, object value)>
    {
        /// <summary>
        /// Gets the script's input parameters as dynamic type for conveniant usage inside of a script file.
        /// </summary>
        dynamic AsDynamic();

        /// <summary>
        /// Update the script's parameter values.
        /// </summary>
        /// <param name="parameters"> The values to be updated. </param>
        void UpdateValues(params (string name, object value)[] parameters);
    }
}