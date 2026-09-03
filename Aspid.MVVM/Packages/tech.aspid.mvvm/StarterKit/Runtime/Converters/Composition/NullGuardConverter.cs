#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Substitutes a fixed result for a <see langword="null"/> input instead of passing it on.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>Settles what a <see langword="null"/> input means regardless of how the inner converter treats it.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Null Guard",
        Tooltip = "Substitutes a fixed result for a null input instead of passing it on")]
    public class NullGuardConverter<TFrom, TTo> : IConverter<TFrom?, TTo?>
    {
        [Tooltip("Run for a non-null value. When empty, the null result is returned.")]
        [TypeSelector]
        [SerializeReference] private IConverter<TFrom?, TTo?>? _inner;

        [Tooltip("Returned when the incoming value is null.")]
        [SerializeField] private TTo? _nullResult;

        protected NullGuardConverter() { }

        /// <param name="inner">The converter to run for a non-null value.</param>
        /// <param name="nullResult">Returned when the incoming value is <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="inner"/> is <see langword="null"/>.</exception>
        public NullGuardConverter(
            IConverter<TFrom?, TTo?> inner,
            TTo? nullResult = default)
        {
            _nullResult = nullResult;
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        /// <summary>
        /// Converts the specified value, short-circuiting <see langword="null"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value, or the null result when the input is <see langword="null"/> or the inner converter is missing.</returns>
        public TTo? Convert(TFrom? value)
        {
            if (value is null) return _nullResult;
            if (_inner is not null) return _inner.Convert(value);

            this.LogError(
                problem: "the inner converter is required, and it is missing",
                consequence: "Returning the null result.");

            return _nullResult;
        }
    }
}
