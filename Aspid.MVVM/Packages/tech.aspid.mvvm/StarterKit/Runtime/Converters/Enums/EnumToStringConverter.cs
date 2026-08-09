using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts an enum value to text.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being converted.</typeparam>
    /// <remarks>
    /// Labelling a state without this means either a switch in the ViewModel or a member name leaking
    /// into the UI. <see cref="EnumNameSource.InspectorName"/> reads the attribute Unity already uses
    /// for the same purpose in the Inspector.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Enum", Name = "Enum To String", Tooltip = "Converts an enum value to text")]
    public sealed class EnumToStringConverter<TEnum> : IConverter<TEnum, string>
        where TEnum : struct, Enum
    {
        [Tooltip("Where the text comes from.")]
        [SerializeField] private EnumNameSource _source;

        [Tooltip("Returned for a value that is not a declared member.")]
        [SerializeField] private string _fallback = string.Empty;

        public EnumToStringConverter() { }

        /// <param name="source">Where the text comes from.</param>
        /// <param name="fallback">Returned for a value that is not a declared member.</param>
        public EnumToStringConverter(EnumNameSource source, string fallback = "")
        {
            _source = source;
            _fallback = fallback;
        }

        /// <summary>
        /// Converts the specified enum value to text.
        /// </summary>
        /// <param name="value">The enum value to convert.</param>
        /// <returns>The member's text, or the fallback when it is not a declared member.</returns>
        public string Convert(TEnum value)
        {
            var name = Enum.GetName(typeof(TEnum), value);
            if (name is null) return _fallback;
            if (_source is EnumNameSource.Name) return name;

            var field = typeof(TEnum).GetField(name);
            var attributes = field?.GetCustomAttributes(typeof(InspectorNameAttribute), inherit: false);

            return attributes is { Length: > 0 } && attributes[0] is InspectorNameAttribute inspector
                ? inspector.displayName
                : name;
        }
    }
}
