using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts any object to its string representation with optional formatting.
    /// </summary>
    [Serializable]
    public sealed class ObjectToStringConverter : GenericToString<object?>, IConverterObjectToString { }
}