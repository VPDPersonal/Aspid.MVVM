// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The unit <see cref="TimeSpanToNumberConverter"/> measures a duration in.
    /// </summary>
    public enum TimeUnit
    {
        /// <summary>
        /// Whole seconds within the minute.
        /// </summary>
        Seconds,

        /// <summary>
        /// The duration in seconds.
        /// </summary>
        TotalSeconds,

        /// <summary>
        /// The duration in minutes.
        /// </summary>
        TotalMinutes,

        /// <summary>
        /// The duration in hours.
        /// </summary>
        TotalHours,

        /// <summary>
        /// The duration in days.
        /// </summary>
        TotalDays,
    }
}
