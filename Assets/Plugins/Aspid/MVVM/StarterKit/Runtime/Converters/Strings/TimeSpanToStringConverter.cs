#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class TimeSpanToStringConverter : GenericToString<TimeSpan>, IConverterTimeSpanToString { }
}