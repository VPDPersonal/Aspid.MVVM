using Aspid.FastTools.Types;
using System;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

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
