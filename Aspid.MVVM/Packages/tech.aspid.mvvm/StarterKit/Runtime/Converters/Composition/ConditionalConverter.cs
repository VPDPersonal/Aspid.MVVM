#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Routes a value to one of two converters based on a predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value being converted.</typeparam>
    /// <remarks>An empty branch passes the value through; a branch without a predicate is reported.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Conditional",
        Tooltip = "Routes a value to one of two converters based on a predicate")]
    public class ConditionalConverter<T> : IConverter<T?, T?>
    {
        [Tooltip("Decides which branch a value takes. Required when a branch is set.")]
        [SerializeReference] private IConverter<T?, bool>? _predicate;

        [Tooltip("Applied when the predicate is true. When empty, the value passes through.")]
        [SerializeReference] private IConverter<T?, T?>? _then;

        [Tooltip("Applied when the predicate is false. When empty, the value passes through.")]
        [SerializeReference] private IConverter<T?, T?>? _else;

        protected ConditionalConverter() { }

        /// <param name="predicate">Decides which branch a value takes.</param>
        /// <param name="then">Applied when the predicate is <see langword="true"/>. When empty, the value passes through.</param>
        /// <param name="else">Applied when the predicate is <see langword="false"/>. When empty, the value passes through.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> is <see langword="null"/>.</exception>
        public ConditionalConverter(
            IConverter<T?, bool> predicate,
            IConverter<T?, T?>? then,
            IConverter<T?, T?>? @else)
        {
            _then = then;
            _else = @else;
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Converts the specified value using the branch the predicate selects.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the selected branch, or the value unchanged when that branch or the predicate is empty.</returns>
        public T? Convert(T? value)
        {
            if (_predicate is null)
            {
                if (_then is null && _else is null) return value;

                this.LogError(
                    problem: "a branch is configured, but the predicate that selects it is missing",
                    consequence: "Returning the input value unchanged.");

                return value;
            }

            var branch = _predicate.Convert(value) ? _then : _else;
            return branch is null ? value : branch.Convert(value);
        }
    }
}
