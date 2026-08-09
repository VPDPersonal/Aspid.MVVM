using Aspid.FastTools.Types;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="GenericToString{TFrom}"/> for any object, with optional formatting.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Object To String", Tooltip = "for any object, with optional formatting")]
    public sealed class ObjectToStringConverter : GenericToString<object?>, IConverterObjectToString
    {
        public ObjectToStringConverter() { }

        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        public ObjectToStringConverter(string format)
            : base(format) { }
    }
}
