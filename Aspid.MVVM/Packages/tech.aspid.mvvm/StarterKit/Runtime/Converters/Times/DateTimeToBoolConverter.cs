using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Compares a <see cref="DateTime"/> with a reference moment.
    /// </summary>
    /// <remarks>Gating on "the event has started" or "the cooldown has expired".</remarks>
    [Serializable]
    public sealed class DateTimeToBoolConverter : IConverter<DateTime, bool>
    {
        [Tooltip("How the bound moment is compared with the reference.")]
        [SerializeField] private Comparisons _comparison = Comparisons.GreaterThan;

        [Tooltip("Compare against the current time rather than the moment below.")]
        [SerializeField] private bool _compareToNow = true;

        [Tooltip("Use UTC when comparing against the current time.")]
        [SerializeField] private bool _useUtcNow;

        [Tooltip("Ticks of the moment compared against when not using the current time.")]
        [SerializeField] private long _referenceTicks;

        /// <remarks>Default: comparing against now.</remarks>
        public DateTimeToBoolConverter() { }

        /// <param name="comparison">How the bound moment is compared with the reference.</param>
        /// <param name="reference">The moment compared against. When <see langword="null"/>, the current time is used.</param>
        public DateTimeToBoolConverter(Comparisons comparison, DateTime? reference = null)
        {
            _comparison = comparison;
            _compareToNow = reference is null;
            _referenceTicks = reference?.Ticks ?? 0L;
        }

        /// <summary>
        /// Compares the specified moment with the reference.
        /// </summary>
        /// <param name="value">The moment to compare.</param>
        /// <returns>The result of the comparison.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the comparison is not a declared value.</exception>
        public bool Convert(DateTime value)
        {
            var reference = _compareToNow
                ? (_useUtcNow ? DateTime.UtcNow : DateTime.Now)
                : new DateTime(_referenceTicks);

            var order = value.CompareTo(reference);

            return _comparison switch
            {
                Comparisons.Equal => order == 0,
                Comparisons.Inequality => order != 0,
                Comparisons.LessThan => order < 0,
                Comparisons.GreaterThan => order > 0,
                Comparisons.LessThanOrEqual => order <= 0,
                Comparisons.GreaterThanOrEqual => order >= 0,
                _ => throw new ArgumentOutOfRangeException(nameof(_comparison), _comparison, null)
            };
        }
    }
}
