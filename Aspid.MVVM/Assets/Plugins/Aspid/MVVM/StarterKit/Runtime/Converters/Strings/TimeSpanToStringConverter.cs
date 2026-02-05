using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class TimeSpanToStringConverter : GenericToString<TimeSpan>, IConverterTimeSpanToString { }
}