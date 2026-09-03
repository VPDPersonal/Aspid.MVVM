#nullable enable
using TMPro;
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Builds the option list of a dropdown out of an enum's members.
    /// </summary>
    /// <remarks>
    /// The option list depends on the enum type, not the value, so it is built once per type and
    /// reused; editing the entries afterward does not rebuild it.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Enum/To Collection",
        Name = "To Dropdown Options",
        Tooltip = "Builds the option list of a dropdown out of an enum's members")]
    public sealed class EnumToDropdownOptionDataConverter : IConverter<Enum?, IEnumerable<TMP_Dropdown.OptionData>>
    {
        [Tooltip("Labels and icons per member. Members not listed use their name.")]
        [SerializeField] private OptionEntry[] _entries = Array.Empty<OptionEntry>();

        [Tooltip("Use the InspectorName attribute for members the entries do not cover.")]
        [SerializeField] private bool _useInspectorNames = true;

        [NonSerialized] private Type? _builtType;
        [NonSerialized] private List<TMP_Dropdown.OptionData>? _options;

        /// <remarks>Default: every member by its name, with its InspectorName honored.</remarks>
        public EnumToDropdownOptionDataConverter() { }

        /// <param name="entries">Labels and icons per member.</param>
        /// <param name="useInspectorNames">
        /// Whether to use the <see cref="InspectorNameAttribute"/> for members
        /// <paramref name="entries"/> does not cover.
        /// </param>
        public EnumToDropdownOptionDataConverter(
            OptionEntry[]? entries,
            bool useInspectorNames = true)
        {
            _useInspectorNames = useInspectorNames;
            _entries = entries is null ? Array.Empty<OptionEntry>() : (OptionEntry[])entries.Clone();
        }

        /// <summary>
        /// Builds the option list for the type of the specified value.
        /// </summary>
        /// <param name="value">Any member of the enum whose options are wanted.</param>
        /// <returns>
        /// One option per member, in declaration order. The same list is returned while the type is
        /// unchanged, so it must not be mutated by the caller. A <see langword="null"/> value carries
        /// no type to build from and answers with an empty list silently: a ViewModel with nothing
        /// selected is a state, not a mistake.
        /// </returns>
        public IEnumerable<TMP_Dropdown.OptionData> Convert(Enum? value)
        {
            if (value is null) return Array.Empty<TMP_Dropdown.OptionData>();

            var type = value.GetType();
            var names = Enum.GetNames(type);

            ReportUnusableEntries(type, names);

            if (_builtType == type && _options is not null) return _options;

            _builtType = type;
            _options = new List<TMP_Dropdown.OptionData>(names.Length);

            foreach (var name in names)
            {
                var entry = FindEntry(name);

                // The constructor taking text and image also takes a color in newer TextMeshPro.
                _options.Add(new TMP_Dropdown.OptionData
                {
                    text = LabelFor(type, name, entry),
                    image = entry?.Icon,
                });
            }

            return _options;
        }

        // Reported before the cache check so a bad entry is logged on every conversion.
        private void ReportUnusableEntries(Type type, string[] names)
        {
            for (var i = 0; i < _entries.Length; i++)
            {
                var name = _entries[i].Name;

                if (FirstIndexOf(name) < i)
                {
                    this.LogError(
                        problem: $"{name.Describe()} is listed more than once in the entries",
                        consequence: "Using the first entry that names it.");

                    continue;
                }

                if (Array.IndexOf(names, name) < 0)
                {
                    this.LogError(
                        problem: $"{name.Describe()} is not a member of {type.Name}",
                        consequence: "Leaving that entry out of the options.");
                }
            }
        }

        private string LabelFor(Type type, string name, OptionEntry? entry)
        {
            if (entry is { Label: { Length: > 0 } label }) return label;

            if (!_useInspectorNames) return name;

            var field = type.GetField(name);
            var attributes = field?.GetCustomAttributes(typeof(InspectorNameAttribute), inherit: false);

            return attributes is { Length: > 0 } && attributes[0] is InspectorNameAttribute inspector
                ? inspector.displayName
                : name;
        }

        private OptionEntry? FindEntry(string name)
        {
            var index = FirstIndexOf(name);
            return index < 0 ? null : _entries[index];
        }

        private int FirstIndexOf(string? name)
        {
            for (var i = 0; i < _entries.Length; i++)
            {
                if (string.Equals(_entries[i].Name, name, StringComparison.Ordinal))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// The label and icon authored for one enum member.
        /// </summary>
        [Serializable]
        public struct OptionEntry
        {
            /// <summary>
            /// Gets the member name this entry belongs to.
            /// </summary>
            [field: Tooltip("The member name this entry belongs to.")]
            [field: SerializeField]
            public string Name { get; private set; }

            /// <summary>
            /// Gets the text shown for the member. When empty, the member name is used.
            /// </summary>
            [field: Tooltip("The text shown for it. When empty, the member name is used.")]
            [field: SerializeField]
            public string Label { get; private set; }

            /// <summary>
            /// Gets the icon shown beside the member.
            /// </summary>
            [field: Tooltip("The icon shown beside it.")]
            [field: SerializeField]
            public Sprite? Icon { get; private set; }

            /// <param name="name">
            /// The member name this entry belongs to. A name the enum does not declare, and a name
            /// listed twice, are reported as errors on every conversion.
            /// </param>
            /// <param name="label">The text shown for it, or <see langword="null"/> to show the member name.</param>
            /// <param name="icon">The icon shown beside it.</param>
            public OptionEntry(
                string name,
                string? label = null,
                Sprite? icon = null)
            {
                Name = name;
                Icon = icon;
                Label = label ?? string.Empty;
            }
        }
    }
}
