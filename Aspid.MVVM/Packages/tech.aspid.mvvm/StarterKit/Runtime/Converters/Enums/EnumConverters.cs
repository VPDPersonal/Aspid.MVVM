using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// One entry of an <see cref="EnumToValueConverter{TEnum, T}"/> map.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being mapped from.</typeparam>
    /// <typeparam name="T">The type being mapped to.</typeparam>
    [Serializable]
    public struct EnumEntry<TEnum, T>
        where TEnum : struct, Enum
    {
        /// <summary>The enum value this entry matches.</summary>
        [Tooltip("The enum value this entry matches.")]
        public TEnum Key;

        /// <summary>The value returned for <see cref="Key"/>.</summary>
        [Tooltip("The value returned for the key.")]
        public T Value;
    }

    /// <summary>
    /// Maps an enum value to an authored value.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being mapped from.</typeparam>
    /// <typeparam name="T">The type being mapped to.</typeparam>
    /// <remarks>
    /// The <c>Enum</c> binder family holds this map in a binder subclass, which means the map cannot
    /// be reused between the icon that shows a state and the colour that tints it. As a converter it
    /// is data, so it can be shared — and as a <see cref="ConverterAsset{TFrom, TTo}"/> it can be
    /// shared across scenes.
    /// </remarks>
    [Serializable]
    public sealed class EnumToValueConverter<TEnum, T> : IConverter<TEnum, T>
        where TEnum : struct, Enum
    {
        [Tooltip("The value returned for each enum member. Members not listed use the fallback.")]
        [SerializeField] private EnumEntry<TEnum, T>[] _map = Array.Empty<EnumEntry<TEnum, T>>();

        [Tooltip("Returned for an enum member the map does not list.")]
        [SerializeField] private T _fallback = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToValueConverter{TEnum, T}"/> class with an empty map.
        /// </summary>
        public EnumToValueConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToValueConverter{TEnum, T}"/> class.
        /// </summary>
        /// <param name="map">The value returned for each enum member.</param>
        /// <param name="fallback">Returned for a member <paramref name="map"/> does not list.</param>
        public EnumToValueConverter(EnumEntry<TEnum, T>[]? map, T fallback = default!)
        {
            _map = map ?? Array.Empty<EnumEntry<TEnum, T>>();
            _fallback = fallback;
        }

        /// <summary>
        /// Looks the specified enum value up in the map.
        /// </summary>
        /// <param name="value">The enum value to look up.</param>
        /// <returns>The mapped value, or the fallback when the map does not list it.</returns>
        public T Convert(TEnum value)
        {
            if (_map is null) return _fallback;

            // A linear scan beats a dictionary here: these maps are a handful of entries, and a
            // dictionary would have to be rebuilt after every deserialization anyway.
            for (var i = 0; i < _map.Length; i++)
                if (EqualityComparer<TEnum>.Default.Equals(_map[i].Key, value))
                    return _map[i].Value;

            return _fallback;
        }
    }

    /// <summary>
    /// How <see cref="EnumToBoolConverter{TEnum}"/> tests a bound enum value.
    /// </summary>
    public enum EnumMatch
    {
        /// <summary>The value must equal the target.</summary>
        Equals,

        /// <summary>The value must differ from the target.</summary>
        NotEquals,

        /// <summary>The value must have every flag the target has.</summary>
        HasAllFlags,

        /// <summary>The value must have at least one flag the target has.</summary>
        HasAnyFlag,
    }

    /// <summary>
    /// Tests an enum value against an authored one.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being tested.</typeparam>
    /// <remarks>
    /// "Show this panel while the state is Loading" needed a boolean property per state on the
    /// ViewModel, one for every state any View cared about.
    /// </remarks>
    [Serializable]
    public sealed class EnumToBoolConverter<TEnum> : IConverter<TEnum, bool>
        where TEnum : struct, Enum
    {
        [Tooltip("The enum value the bound one is tested against.")]
        [SerializeField] private TEnum _target;

        [Tooltip("How the bound value is tested against the target.")]
        [SerializeField] private EnumMatch _match;

        [Tooltip("Invert the result.")]
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToBoolConverter{TEnum}"/> class with default settings.
        /// </summary>
        public EnumToBoolConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToBoolConverter{TEnum}"/> class.
        /// </summary>
        /// <param name="target">The enum value the bound one is tested against.</param>
        /// <param name="match">How the bound value is tested against <paramref name="target"/>.</param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public EnumToBoolConverter(TEnum target, EnumMatch match = EnumMatch.Equals, bool isInvert = false)
        {
            _target = target;
            _match = match;
            _isInvert = isInvert;
        }

        /// <summary>
        /// Tests the specified enum value against the authored one.
        /// </summary>
        /// <param name="value">The enum value to test.</param>
        /// <returns>The result of the test, inverted when configured.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the match mode is not a declared value.</exception>
        public bool Convert(TEnum value)
        {
            var target = EnumNumber(_target);
            var actual = EnumNumber(value);

            var matched = _match switch
            {
                EnumMatch.Equals => actual == target,
                EnumMatch.NotEquals => actual != target,
                EnumMatch.HasAllFlags => (actual & target) == target,
                EnumMatch.HasAnyFlag => (actual & target) != 0,
                _ => throw new ArgumentOutOfRangeException(nameof(_match), _match, null)
            };

            return _isInvert ? !matched : matched;
        }

        // Converting through long rather than Enum.HasFlag keeps the comparison allocation-free:
        // HasFlag boxes both operands on every call.
        private static long EnumNumber(TEnum value) => System.Convert.ToInt64(value);
    }

    /// <summary>
    /// Converts an enum value to its underlying integer.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being converted.</typeparam>
    /// <remarks>
    /// A dropdown's selected index is an <see cref="int"/>, so binding one to an enum property took
    /// a conversion the ViewModel had to expose itself.
    /// </remarks>
    [Serializable]
    public sealed class EnumToIntConverter<TEnum> : ITwoWayConverter<TEnum, int>
        where TEnum : struct, Enum
    {
        /// <summary>
        /// Converts the specified enum value to its underlying integer.
        /// </summary>
        /// <param name="value">The enum value to convert.</param>
        /// <returns>The underlying integer.</returns>
        public int Convert(TEnum value) => System.Convert.ToInt32(value);

        /// <summary>
        /// Converts an integer back to the enum value it represents.
        /// </summary>
        /// <param name="value">The integer to convert.</param>
        /// <returns>The enum value, which need not be a declared member.</returns>
        public TEnum ConvertBack(int value) => (TEnum)Enum.ToObject(typeof(TEnum), value);
    }

    /// <summary>
    /// Where <see cref="EnumToStringConverter{TEnum}"/> takes the text it returns.
    /// </summary>
    public enum EnumNameSource
    {
        /// <summary>The member name as written in code.</summary>
        Name,

        /// <summary>The <see cref="InspectorNameAttribute"/> on the member, falling back to its name.</summary>
        InspectorName,
    }

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
    public sealed class EnumToStringConverter<TEnum> : IConverter<TEnum, string>
        where TEnum : struct, Enum
    {
        [Tooltip("Where the text comes from.")]
        [SerializeField] private EnumNameSource _source;

        [Tooltip("Returned for a value that is not a declared member.")]
        [SerializeField] private string _fallback = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToStringConverter{TEnum}"/> class with default settings.
        /// </summary>
        public EnumToStringConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumToStringConverter{TEnum}"/> class.
        /// </summary>
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
