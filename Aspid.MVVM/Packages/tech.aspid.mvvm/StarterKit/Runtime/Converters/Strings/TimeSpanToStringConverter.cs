using Aspid.FastTools.Types;
using System;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="GenericToStringConverter{TFrom}"/> for <see cref="TimeSpan"/> values, with optional formatting.
    /// </summary>
    /// <remarks>
    /// The format is a <b>composite</b> format string, so a <see cref="TimeSpan"/> pattern has to be
    /// wrapped in a placeholder: <c>"{0:mm\\:ss}"</c> yields <c>05:05</c>, while the bare
    /// <c>"mm\\:ss"</c> that <see cref="TimeSpan.ToString(string)"/> would take comes back as itself.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Time Span To String", Tooltip = "Writes a TimeSpan as text, with optional formatting")]
    public sealed class TimeSpanToStringConverter : GenericToStringConverter<TimeSpan>, IConverterTimeSpanToString
    {
        public TimeSpanToStringConverter() { }

        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        public TimeSpanToStringConverter(string format)
            : base(format) { }
    }
}
