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
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectToStringConverter"/> class with no formatting.
        /// </summary>
        public ObjectToStringConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectToStringConverter"/> class.
        /// </summary>
        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        public ObjectToStringConverter(string format)
            : base(format) { }
    }
}
