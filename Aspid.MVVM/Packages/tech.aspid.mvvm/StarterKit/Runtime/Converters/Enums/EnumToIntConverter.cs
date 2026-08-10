using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts an enum value to an integer and back.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being converted.</typeparam>
    /// <remarks>
    /// A dropdown's selected index is an <see cref="int"/>, so binding one to an enum property took
    /// a conversion the ViewModel had to expose itself.
    /// <para>
    /// A dropdown numbers its options 0, 1, 2 whatever its entries stand for, so an enum that skips
    /// values — <c>None = 0, Bronze = 10, Silver = 20</c> — selects the wrong row while the
    /// underlying number is passed through. The position mode counts members instead, which is what
    /// an index means.
    /// </para>
    /// <para>
    /// Only a binder in TwoWay or OneWayToSource calls <see cref="ConvertBack"/>. An integer source
    /// driving an enum property one way wants <see cref="IntToEnumConverter{TEnum}"/>, which also
    /// refuses an integer that names no member rather than casting it.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Enum", Name = "Enum To Int", Tooltip = "Converts an enum value to an integer and back")]
    public sealed class EnumToIntConverter<TEnum> : ITwoWayConverter<TEnum, int>
        where TEnum : struct, Enum
    {
        [Tooltip("Count the member's position in the enum instead of reading its underlying value. A dropdown index is a position.")]
        [SerializeField] private bool _byIndexNotValue;

        [Tooltip("Returned by the reverse direction for a position outside the enum. Unused while the position mode is off.")]
        [SerializeField] private TEnum _fallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToIntConverter{TEnum}"/> class reading the underlying value.
        /// </summary>
        public EnumToIntConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToIntConverter{TEnum}"/> class.
        /// </summary>
        /// <param name="byIndexNotValue">
        /// If <see langword="true"/>, converts to and from the member's position in the enum rather
        /// than its underlying value.
        /// </param>
        /// <param name="fallback">Returned by <see cref="ConvertBack"/> for a position outside the enum.</param>
        public EnumToIntConverter(bool byIndexNotValue, TEnum fallback = default)
        {
            _byIndexNotValue = byIndexNotValue;
            _fallback = fallback;
        }

        /// <summary>
        /// Converts the specified enum value to an integer.
        /// </summary>
        /// <param name="value">The enum value to convert.</param>
        /// <returns>
        /// The underlying integer, or the member's position under the position mode — where a value
        /// that is not a declared member gives -1, which a dropdown reads as no selection.
        /// </returns>
        /// <exception cref="OverflowException">
        /// Thrown when the underlying value does not fit in an <see cref="int"/>, which only a
        /// <see cref="long"/>- or <see cref="ulong"/>-backed enum can manage.
        /// </exception>
        public int Convert(TEnum value) => _byIndexNotValue
            ? EnumMembers<TEnum>.IndexOf(value)
            : System.Convert.ToInt32(value);

        /// <summary>
        /// Converts an integer back to the enum value it stands for.
        /// </summary>
        /// <param name="value">The integer to convert.</param>
        /// <returns>
        /// The enum value. Read as an underlying number it need not be a declared member — that is
        /// how a flag combination survives the round trip. Read as a position, one outside the enum
        /// gives the fallback.
        /// </returns>
        public TEnum ConvertBack(int value) => _byIndexNotValue
            ? EnumMembers<TEnum>.At(value, _fallback)
            : (TEnum)Enum.ToObject(typeof(TEnum), value);
    }
}
