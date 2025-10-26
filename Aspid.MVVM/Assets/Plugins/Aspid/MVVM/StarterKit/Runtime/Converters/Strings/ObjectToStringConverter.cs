using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class ObjectToStringConverter : GenericToString<object?>, IConverterObjectToString { }
}