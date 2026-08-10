#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a bound flags value with an authored mask.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being combined.</typeparam>
    /// <remarks>
    /// One panel shows the elemental damage of an attack while another shows the physical part of the
    /// same value. Both read one property, and the choice of which flags each cares about is the
    /// View's business — as a mask on the binder it is authored beside the panel it belongs to,
    /// rather than as a second filtered property on the ViewModel.
    /// <para>
    /// Chained ahead of <see cref="EnumFlagsToStringConverter{TEnum}"/> or
    /// <see cref="EnumToBoolConverter{TEnum}"/> it narrows what those see, which is what makes it
    /// worth a node of its own rather than an option on each of them.
    /// </para>
    /// <para>
    /// On an enum not marked <see cref="FlagsAttribute"/> the value passes through untouched: its
    /// number is one member's value rather than a set of bits, and combining it would blank the
    /// value or produce a number no member declares.
    /// </para>
    /// <para>
    /// The previous result is reused while the value, the mask and the operation are unchanged: an
    /// enum has no non-boxing route back from its bits, so a mask applied on every push would
    /// allocate on every notification. Only the last input is remembered, so a source alternating
    /// between two values pays for both.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Enum", Name = "Enum Mask", Tooltip = "Combines a bound flags value with an authored mask")]
    public sealed class EnumMaskConverter<TEnum> : IConverter<TEnum, TEnum>
        where TEnum : struct, Enum
    {
        [Tooltip("The flags the bound value is combined with. Nothing is combined on an enum not marked [Flags], where the value passes through unchanged.")]
        [SerializeField] private TEnum _mask;

        [Tooltip("What is done with the flags the mask names.")]
        [SerializeField] private EnumMaskOperation _operation;

        [NonSerialized] private bool _hasCache;
        [NonSerialized] private TEnum _cachedValue;
        [NonSerialized] private TEnum _cachedMask;
        [NonSerialized] private TEnum _cachedResult;
        [NonSerialized] private EnumMaskOperation _cachedOperation;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumMaskConverter{TEnum}"/> class with an empty mask.
        /// </summary>
        public EnumMaskConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumMaskConverter{TEnum}"/> class.
        /// </summary>
        /// <param name="mask">The flags the bound value is combined with.</param>
        /// <param name="operation">What is done with the flags <paramref name="mask"/> names.</param>
        public EnumMaskConverter(TEnum mask, EnumMaskOperation operation = EnumMaskOperation.And)
        {
            _mask = mask;
            _operation = operation;
        }

        /// <summary>
        /// Applies the authored mask to the specified value.
        /// </summary>
        /// <param name="value">The value to combine with the mask.</param>
        /// <returns>
        /// The combined value. It need not be a declared member: a combination of flags is a legal
        /// value the member list does not hold, which is the whole point of the type. On an enum not
        /// marked <see cref="FlagsAttribute"/> the value is returned unchanged.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the operation is not a declared value.</exception>
        public TEnum Convert(TEnum value)
        {
            // Combining bit by bit only means anything on an enum whose members are bits. On any
            // other enum the number is one member's value: masking it would either blank it or hand
            // the View a number no member declares, which is not a reading of the value at all.
            if (!EnumBits<TEnum>.IsFlags) return value;

            var comparer = EqualityComparer<TEnum>.Default;

            // The mask and the operation are serialized, so the Inspector can change either between
            // two pushes of the same value. Comparing them costs nothing next to what it guards.
            if (_hasCache
                && _cachedOperation == _operation
                && comparer.Equals(_cachedValue, value)
                && comparer.Equals(_cachedMask, _mask))
                return _cachedResult;

            var bits = EnumBits<TEnum>.BitsOf(value);
            var mask = EnumBits<TEnum>.BitsOf(_mask);

            var combined = _operation switch
            {
                EnumMaskOperation.And => bits & mask,
                EnumMaskOperation.Or => bits | mask,
                EnumMaskOperation.Xor => bits ^ mask,
                // The complement sets every bit above the enum's own width, and the AND keeps them
                // whenever the value carries them too — a member holding the sign bit of a signed
                // underlying type sign-extends into all of them. FromBits truncates to that width,
                // which is what makes this safe; the AND alone does not.
                EnumMaskOperation.Clear => bits & ~mask,
                _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
            };

            _cachedResult = EnumBits<TEnum>.FromBits(combined);
            _cachedOperation = _operation;
            _cachedValue = value;
            _cachedMask = _mask;
            _hasCache = true;

            return _cachedResult;
        }
    }
}
