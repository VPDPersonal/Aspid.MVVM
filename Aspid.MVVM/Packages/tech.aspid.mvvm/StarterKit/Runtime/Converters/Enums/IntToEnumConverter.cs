using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts an integer to the enum value it stands for.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being converted to.</typeparam>
    /// <remarks>
    /// An integer that names no member is refused rather than cast: a cast would hand the View a value
    /// no <c>switch</c> in the game has a case for, and the symptom would surface far from the number
    /// that caused it. An enum marked <see cref="FlagsAttribute"/> still accepts any combination of its
    /// declared flags.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Enum", Name = "Int To Enum", Tooltip = "Converts an integer to the enum value it stands for")]
    public sealed class IntToEnumConverter<TEnum> : IConverter<int, TEnum>
        where TEnum : struct, Enum
    {
        [Tooltip("Read the integer as a member's position in the enum instead of its underlying value. A dropdown index is a position.")]
        [SerializeField] private bool _byIndexNotValue;

        [Tooltip("Returned for an integer that names no member.")]
        [SerializeField] private TEnum _fallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntToEnumConverter{TEnum}"/> class reading the underlying value.
        /// </summary>
        public IntToEnumConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntToEnumConverter{TEnum}"/> class.
        /// </summary>
        /// <param name="byIndexNotValue">
        /// If <see langword="true"/>, reads the integer as a member's position in the enum rather
        /// than as an underlying value.
        /// </param>
        /// <param name="fallback">Returned for an integer that names no member.</param>
        public IntToEnumConverter(bool byIndexNotValue, TEnum fallback = default)
        {
            _byIndexNotValue = byIndexNotValue;
            _fallback = fallback;
        }

        /// <summary>
        /// Converts the specified integer to an enum value.
        /// </summary>
        /// <param name="value">The integer to convert.</param>
        /// <returns>The enum value it stands for, or the fallback when it stands for none.</returns>
        public TEnum Convert(int value) => _byIndexNotValue
            ? EnumMembers<TEnum>.At(value, _fallback)
            : EnumMembers<TEnum>.FromNumber(value, _fallback);
    }
}
