using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Substitutes a fixed result for a <see langword="null"/> input instead of passing it on.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>
    /// Converters disagree on what a <see langword="null"/> input means — return
    /// <see langword="null"/>, throw, or format it — and the disagreement is not visible from the
    /// Inspector. Wrapping one settles the question at the point of use.
    /// </remarks>
    [Serializable]
    public sealed class NullGuardConverter<TFrom, TTo> : IConverter<TFrom?, TTo?>
    {
        [Tooltip("The converter to run for a non-null value. When empty, the null result is used for every value.")]
        [SerializeReference] private IConverter<TFrom?, TTo?>? _inner;

        [Tooltip("Returned when the incoming value is null.")]
        [SerializeField] private TTo? _nullResult;

        public NullGuardConverter() { }

        /// <param name="inner">The converter to run for a non-null value.</param>
        /// <param name="nullResult">Returned when the incoming value is <see langword="null"/>.</param>
        public NullGuardConverter(IConverter<TFrom?, TTo?>? inner, TTo? nullResult = default)
        {
            _inner = inner;
            _nullResult = nullResult;
        }

        /// <summary>
        /// Converts the specified value, short-circuiting <see langword="null"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value, or the configured null result.</returns>
        public TTo? Convert(TFrom? value) =>
            value is null || _inner is null ? _nullResult : _inner.Convert(value);
    }
}
