// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The shape <see cref="SecondsToTimeStringConverter"/> writes a duration in.
    /// </summary>
    public enum TimeLayout
    {
        /// <summary>
        /// Seconds only.
        /// </summary>
        Seconds,

        /// <summary>
        /// mm:ss.
        /// </summary>
        MinutesSeconds,

        /// <summary>
        /// h:mm:ss.
        /// </summary>
        HoursMinutesSeconds,

        /// <summary>
        /// d:hh:mm:ss.
        /// </summary>
        DaysHoursMinutesSeconds,

        /// <summary>
        /// The shortest layout that fits the value.
        /// </summary>
        Auto,
    }
}
