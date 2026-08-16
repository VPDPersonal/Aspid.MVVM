#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using Aspid.FastTools.Enums;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IConverterEnumToDropdownOptionData"/> that builds one
    /// <see cref="TMP_Dropdown.OptionData"/> per member of the bound enum type, resolving the label and
    /// the sprite of each option through a configurable <see cref="EnumValues{TValue}"/> lookup table.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Options are emitted in declaration order of the enum type, which is the same order
    /// <see cref="DropdownOptionsByEnumMonoBinder"/> uses without a converter — so the selected index
    /// keeps matching the enum member it did before the converter was assigned.
    /// </para>
    /// <para>
    /// Members missing from the lookup table fall back to its configured default value: an entry without
    /// a sprite yields an option without an image, and an entry with an empty label yields the name of
    /// the enum member.
    /// </para>
    /// <para>
    /// Sprites are only visible when the dropdown template carries an item image — the built-in TextMeshPro
    /// dropdown prefab does, a hand-built template may not.
    /// </para>
    /// </remarks>
    [Serializable]
    public sealed class EnumToDropdownOptionDataConverter : IConverterEnumToDropdownOptionData
    {
        [Tooltip("Lookup table mapping each enum value to the label and the sprite of its dropdown option.")]
        [SerializeField] private EnumValues<DropdownOption>? _options;

        /// <summary>
        /// Builds the option data for every member of the enum type of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The bound enum value received from the ViewModel; only its type is used.</param>
        /// <returns>
        /// One <see cref="TMP_Dropdown.OptionData"/> per enum member in declaration order,
        /// or an empty sequence when <paramref name="value"/> is <see langword="null"/>.
        /// </returns>
        public IEnumerable<TMP_Dropdown.OptionData> Convert(Enum? value)
        {
            if (value is null) yield break;

            foreach (Enum member in Enum.GetValues(value.GetType()))
            {
                var option = _options?.GetValue(member) ?? default;
                var text = string.IsNullOrWhiteSpace(option.Text)
                    ? member.ToString() 
                    : option.Text;

                yield return new TMP_Dropdown.OptionData(text) { image = option.Sprite };
            }
        }

        /// <summary>
        /// Serializable pair of a label and an optional sprite describing a single dropdown option.
        /// </summary>
        [Serializable]
        private struct DropdownOption
        {
            [Tooltip("The label shown for the option. When empty, the name of the enum value is used.")]
            [SerializeField] private string? _text;

            [Tooltip("The sprite shown next to the label. When empty, the option is shown without an image.")]
            [SerializeField] private Sprite? _sprite;

            /// <summary>
            /// Gets the label shown for the option.
            /// Empty when the name of the enum value should be used instead.
            /// </summary>
            public string? Text => _text;

            /// <summary>
            /// Gets the sprite shown next to the label, or <see langword="null"/> when the option has no image.
            /// </summary>
            public Sprite? Sprite => _sprite;
        }
    }
}
#endif
