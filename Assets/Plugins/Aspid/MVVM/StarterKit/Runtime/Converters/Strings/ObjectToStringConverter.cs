#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class ObjectToStringConverter : GenericToString<object?>, IConverterObjectToString { }
}