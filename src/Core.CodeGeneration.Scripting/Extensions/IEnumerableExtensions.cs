namespace CustomCode.Core.CodeGeneration.Scripting.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for the <see cref="IEnumerable{T}"/> interface.
    /// </summary>
    public static class IEnumerableExtensions
    {
        #region Logic

        /// <summary>
        /// Apply an <paramref name="action"/> to each <paramref name="source"/> element.
        /// </summary>
        /// <typeparam name="T"> The type of a single <paramref name="source"/> element. </typeparam>
        /// <param name="source"> The source collection to operate on. </param>
        /// <param name="action"> The delegate that should by applied to each <paramref name="source"/> element. </param>
        public static void Apply<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                return;
            }

            foreach (var element in source)
            {
                action(element);
            }
        }

        #endregion
    }
}