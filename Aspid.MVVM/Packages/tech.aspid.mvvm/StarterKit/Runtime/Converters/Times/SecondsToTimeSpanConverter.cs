using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a number of seconds to a <see cref="TimeSpan"/>.
    /// </summary>
    [Serializable]
    public sealed class SecondsToTimeSpanConverter : ITwoWayConverter<float, TimeSpan>
    {
        /// <summary>
        /// Converts the specified seconds to a duration.
        /// </summary>
        /// <param name="value">The number of seconds.</param>
        /// <returns>The duration.</returns>
        public TimeSpan Convert(float value) => TimeSpan.FromSeconds(value);

        /// <summary>
        /// Converts a duration back to seconds.
        /// </summary>
        /// <param name="value">The duration.</param>
        /// <returns>The number of seconds.</returns>
        public float ConvertBack(TimeSpan value) => (float)value.TotalSeconds;
    }
}
