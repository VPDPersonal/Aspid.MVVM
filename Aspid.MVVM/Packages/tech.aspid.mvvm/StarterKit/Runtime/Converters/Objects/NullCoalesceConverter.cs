#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Diagnostics.CodeAnalysis;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Substitutes an authored value for a <see langword="null"/> one.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <remarks>A destroyed <see cref="UnityEngine.Object"/> counts as missing, which plain <c>??</c> would not catch.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Object",
        Name = "Null Coalesce",
        Tooltip = "Substitutes an authored value for a null one")]
    public class NullCoalesceConverter<T> : IConverter<T?, T?>
        where T : class
    {
        [Tooltip("Returned instead of a null or destroyed value.")]
        [SerializeField] private T? _fallback;

        protected NullCoalesceConverter() { }

        /// <param name="fallback">Returned when the bound value is <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="fallback"/> is <see langword="null"/> or a destroyed <see cref="UnityEngine.Object"/>.
        /// </exception>
        public NullCoalesceConverter(T fallback)
        {
            if (IsMissing(fallback))
                throw new ArgumentNullException(nameof(fallback), "The fallback is missing or destroyed.");

            _fallback = fallback;
        }

        /// <summary>
        /// Returns the specified value, or the fallback when it is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>The value, or the fallback. A missing or destroyed fallback is reported and still returned.</returns>
        public T? Convert(T? value)
        {
            if (!IsMissing(value)) return value;

            if (IsMissing(_fallback))
            {
                this.LogError(
                    problem: "the fallback is missing or destroyed",
                    consequence: "Returning it anyway.");
            }

            return _fallback;
        }

        private static bool IsMissing([NotNullWhen(returnValue: false)] T? value) =>
            value is null || (value is Object unityObject && unityObject == null);
    }
}
