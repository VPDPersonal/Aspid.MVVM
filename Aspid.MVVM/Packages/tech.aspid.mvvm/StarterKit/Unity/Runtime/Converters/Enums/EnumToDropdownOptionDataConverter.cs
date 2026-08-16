#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Builds the option list of a dropdown out of an enum's members.
    /// </summary>
    /// <remarks>
    /// The option list depends on the enum type, not the value, so it is built once per type and
    /// reused. Rebuilding it per push would allocate an <see cref="TMP_Dropdown.OptionData"/> per
    /// member on every notification.
    /// </remarks>
    [Serializable]
    public sealed class EnumToDropdownOptionDataConverter : IConverterEnumToDropdownOptionData
    {
        /// <summary>
        /// The label and icon authored for one enum member.
        /// </summary>
        [Serializable]
        public struct Entry
        {
            /// <summary>The member name this entry belongs to.</summary>
            [Tooltip("The member name this entry belongs to.")]
            public string Name;

            /// <summary>The text shown for it. When empty, the member name is used.</summary>
            [Tooltip("The text shown for it. When empty, the member name is used.")]
            public string Label;

            /// <summary>The icon shown beside it.</summary>
            [Tooltip("The icon shown beside it.")]
            public Sprite? Icon;
        }

        [Tooltip("Labels and icons per member. Members not listed use their name and no icon.")]
        [SerializeField] private Entry[] _entries = Array.Empty<Entry>();

        [Tooltip("Use the InspectorName attribute for members the list above does not cover.")]
        [SerializeField] private bool _useInspectorNames = true;

        [NonSerialized] private Type? _builtType;
        [NonSerialized] private List<TMP_Dropdown.OptionData>? _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToDropdownOptionDataConverter"/> class with no overrides.
        /// </summary>
        public EnumToDropdownOptionDataConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToDropdownOptionDataConverter"/> class.
        /// </summary>
        /// <param name="entries">Labels and icons per member.</param>
        public EnumToDropdownOptionDataConverter(Entry[]? entries)
        {
            _entries = entries ?? Array.Empty<Entry>();
        }

        /// <summary>
        /// Builds the option list for the type of the specified value.
        /// </summary>
        /// <param name="value">Any member of the enum whose options are wanted.</param>
        /// <returns>
        /// One option per member, in declaration order. The same list is returned while the type is
        /// unchanged, so it must not be mutated by the caller.
        /// </returns>
        public IEnumerable<TMP_Dropdown.OptionData> Convert(Enum value)
        {
            if (value is null) return Array.Empty<TMP_Dropdown.OptionData>();

            var type = value.GetType();
            if (_builtType == type && _options is not null) return _options;

            _builtType = type;
            _options = new List<TMP_Dropdown.OptionData>();

            // Built through the properties rather than a constructor: the overload taking text and an
            // image also takes a colour in newer TextMeshPro, and the shape differs between versions.
            foreach (var name in Enum.GetNames(type))
                _options.Add(new TMP_Dropdown.OptionData
                {
                    text = LabelFor(type, name),
                    image = IconFor(name),
                });

            return _options;
        }

        private string LabelFor(Type type, string name)
        {
            var entry = FindEntry(name);
            if (entry is { Label: { Length: > 0 } label }) return label;

            if (!_useInspectorNames) return name;

            var field = type.GetField(name);
            var attributes = field?.GetCustomAttributes(typeof(InspectorNameAttribute), inherit: false);

            return attributes is { Length: > 0 } && attributes[0] is InspectorNameAttribute inspector
                ? inspector.displayName
                : name;
        }

        private Sprite? IconFor(string name) => FindEntry(name)?.Icon;

        private Entry? FindEntry(string name)
        {
            if (_entries is not { Length: > 0 }) return null;

            for (var i = 0; i < _entries.Length; i++)
                if (string.Equals(_entries[i].Name, name, StringComparison.Ordinal))
                    return _entries[i];

            return null;
        }
    }
}
#endif
