#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class TimeSpanToStringConverter : GenericToString<TimeSpan>, IConverterTimeSpanToString { }
}