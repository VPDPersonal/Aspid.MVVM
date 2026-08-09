using Aspid.FastTools.Types;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="GenericToString{TFrom}"/> for <see cref="TimeSpan"/> values, with optional formatting.
    /// </summary>
    /// <remarks>
    /// The format is a <b>composite</b> format string, so a <see cref="TimeSpan"/> pattern has to be
    /// wrapped in a placeholder: <c>"{0:mm\\:ss}"</c> yields <c>05:05</c>, while the bare
    /// <c>"mm\\:ss"</c> that <see cref="TimeSpan.ToString(string)"/> would take comes back as itself.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Time Span To String", Tooltip = "for  values, with optional formatting")]
    public sealed class TimeSpanToStringConverter : GenericToString<TimeSpan>, IConverterTimeSpanToString
    {
        public TimeSpanToStringConverter() { }

        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        public TimeSpanToStringConverter(string format)
            : base(format) { }
    }
}
