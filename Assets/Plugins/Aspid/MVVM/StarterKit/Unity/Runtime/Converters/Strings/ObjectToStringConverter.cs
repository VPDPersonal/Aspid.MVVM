#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class ObjectToStringConverter : GenericToString<object?>, IConverterObjectToString { }
}