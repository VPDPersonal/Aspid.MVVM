using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Routes a value to one of two converters based on a predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value being converted.</typeparam>
    /// <remarks>
    /// Every part is optional, and a missing part means "leave the value alone", so a partially
    /// configured instance degrades to the identity rather than to an error.
    /// </remarks>
    [Serializable]
    public sealed class ConditionalConverter<T> : IConverter<T, T>
    {
        [Tooltip("Decides which branch a value takes. When empty, the value passes through unchanged.")]
        [SerializeReference] private IConverter<T, bool>? _predicate;

        [Tooltip("Applied when the predicate is true. When empty, the value passes through unchanged.")]
        [SerializeReference] private IConverter<T, T>? _then;

        [Tooltip("Applied when the predicate is false. When empty, the value passes through unchanged.")]
        [SerializeReference] private IConverter<T, T>? _else;

        public ConditionalConverter() { }

        /// <param name="predicate">Decides which branch a value takes.</param>
        /// <param name="then">Applied when the predicate is <see langword="true"/>.</param>
        /// <param name="else">Applied when the predicate is <see langword="false"/>.</param>
        public ConditionalConverter(
            IConverter<T, bool>? predicate,
            IConverter<T, T>? then,
            IConverter<T, T>? @else)
        {
            _then = then;
            _else = @else;
            _predicate = predicate;
        }

        /// <summary>
        /// Converts the specified value using the branch the predicate selects.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the selected branch, or the value unchanged when it is empty.</returns>
        public T Convert(T value)
        {
            if (_predicate is null) return value;

            var branch = _predicate.Convert(value) ? _then : _else;
            return branch is null ? value : branch.Convert(value);
        }
    }
}
