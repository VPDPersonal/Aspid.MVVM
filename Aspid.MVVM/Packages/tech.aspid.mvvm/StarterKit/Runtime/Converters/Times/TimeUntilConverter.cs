#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Measures how long there is until a moment.
    /// </summary>
    /// <remarks>The result is only as fresh as the last push; the ViewModel still has to tick.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Time",
        Name = "Time Until",
        Tooltip = "Measures how long there is until a moment")]
    public sealed class TimeUntilConverter : IConverter<DateTime, TimeSpan>
    {
        [Tooltip("Measure an Unspecified-kind moment against UTC rather than local time.")]
        [SerializeField] private bool _useUtcNow;

        [Tooltip("Report a moment that has already passed as zero rather than as a negative duration.")]
        [SerializeField] private bool _clampToZero = true;

        /// <remarks>Default: measuring against local time.</remarks>
        public TimeUntilConverter() { }

        /// <param name="useUtcNow">Whether an <see cref="DateTimeKind.Unspecified"/> moment is measured against UTC rather than local time.</param>
        /// <param name="clampToZero">If <see langword="true"/>, reports a moment already past as zero.</param>
        public TimeUntilConverter(
            bool useUtcNow,
            bool clampToZero = true)
        {
            _useUtcNow = useUtcNow;
            _clampToZero = clampToZero;
        }

        /// <summary>
        /// Measures how long there is until the specified moment.
        /// </summary>
        /// <param name="value">The moment to measure to.</param>
        /// <returns>The duration remaining, negative once the moment has passed unless clamped.</returns>
        public TimeSpan Convert(DateTime value)
        {
            var remaining = value - CurrentTime.For(value, _useUtcNow);

            return _clampToZero && remaining.Ticks < 0L
                ? TimeSpan.Zero
                : remaining;
        }
    }
}
