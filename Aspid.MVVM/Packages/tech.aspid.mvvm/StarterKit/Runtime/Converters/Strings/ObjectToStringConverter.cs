using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="GenericToString{TFrom}"/> for any object, with optional formatting.
    /// </summary>
    [Serializable]
    public sealed class ObjectToStringConverter : GenericToString<object?>, IConverterObjectToString
    {
        public ObjectToStringConverter() { }

        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        public ObjectToStringConverter(string format)
            : base(format) { }
    }
}
