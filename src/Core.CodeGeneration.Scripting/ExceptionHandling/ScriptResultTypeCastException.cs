namespace CustomCode.Core.CodeGeneration.Scripting.ExceptionHandling
{
    using System;

    /// <summary>
    /// Exception that is raised when a result value of a <see cref="Features.IResultCollection"/> was requested
    /// by name and was found, but could not be converted to the requested type.
    /// </summary>
    public sealed class ScriptResultTypeCastException : ScriptingException
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ScriptResultTypeCastException"/> type.
        /// </summary>
        /// <param name="name"> The name of the result value that could not be converted to the <paramref name="requestedType"/>. </param>
        /// <param name="requestedType"> The result value's requested type. </param>
        /// <param name="actualType"> The result value's actual type. </param>
        /// <param name="script"> The script that has thrown the exception. </param>
        public ScriptResultTypeCastException(string name, Type requestedType, Type actualType, IScript script)
            : base(script, $"The result value {name} could not be converted from {actualType.Name} to {requestedType.Name}", "ErrorScriptResultTypeCast")
        {
            ActualType = actualType;
            Name = name;
            RequestedType = requestedType;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the result value's actual type.
        /// </summary>
        public Type ActualType { get; }

        /// <summary>
        /// Gets the name of the result value that could not be found.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the result value's requested type.
        /// </summary>
        public Type RequestedType { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Convert exception data to an object array that can be used via <see cref="string.Format(string, object[])"/>
        /// for localization purposes.
        /// </summary>
        /// <returns> The exception's format items for localization or null. </returns>
        public override object[] GetFormatItems()
        {
            return new object[] { Name, ActualType, RequestedType, Script };
        }

        #endregion
    }
}