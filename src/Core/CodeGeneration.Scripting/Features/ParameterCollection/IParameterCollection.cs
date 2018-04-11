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

        /// <summary>
        /// Validate if supplied script parameters match with the parameters used by the script.
        /// </summary>
        /// <param name="ignoreAdditionalParameters">
        /// True if additional parameters (not used by the script) should not be validated, false otherwise.
        /// </param>
        /// <param name="ignoreMissingParameters">
        /// True if missing parameters (used by the script) should not be validated, false otherwise.
        /// </param>
        /// <param name="parameters"> A collection of supplied script parameters. </param>
        void ValidateParameterNames(
            bool ignoreAdditionalParameters,
            bool ignoreMissingParameters,
            params (string name, object value)[] parameters);
    }
}