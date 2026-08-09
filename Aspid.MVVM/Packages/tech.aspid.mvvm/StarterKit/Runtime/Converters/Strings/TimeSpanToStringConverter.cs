using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts <see cref="TimeSpan"/> values to their string representation with optional formatting.
    /// </summary>
    [Serializable]
    public sealed class TimeSpanToStringConverter : GenericToString<TimeSpan>, IConverterTimeSpanToString
    {
        public TimeSpanToStringConverter() { }

        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        public TimeSpanToStringConverter(string format)
            : base(format) { }
    }
}