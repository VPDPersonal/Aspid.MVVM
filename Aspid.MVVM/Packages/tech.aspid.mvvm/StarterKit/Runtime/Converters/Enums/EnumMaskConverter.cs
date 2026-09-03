#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a bound flags value with an authored mask.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being combined.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Enum",
        Name = "Mask",
        Tooltip = "Combines a bound flags value with an authored mask")]
    public class EnumMaskConverter<TEnum> : IConverter<TEnum, TEnum>
        where TEnum : struct, Enum
    {
        [Tooltip("The flags the bound value is combined with.")]
        [SerializeField] private TEnum _mask;

        [Tooltip("What is done with the flags the mask names.")]
        [SerializeField] private EnumMaskOperation _operation;

        [Tooltip("Returned when the enum is not marked [Flags] or the operation is undeclared.")]
        [SerializeField] private ConverterFallback<TEnum> _fallback = new(default, ConverterFailureMode.ReturnInput);

        [NonSerialized] private bool _hasCache;
        [NonSerialized] private TEnum _cachedValue;
        [NonSerialized] private TEnum _cachedMask;
        [NonSerialized] private TEnum _cachedResult;
        [NonSerialized] private EnumMaskOperation _cachedOperation;

        protected EnumMaskConverter() { }

        /// <param name="mask">The flags the bound value is combined with.</param>
        /// <param name="operation">What is done with the flags <paramref name="mask"/> names.</param>
        /// <param name="fallback">
        /// Returned when the enum is not marked <see cref="FlagsAttribute"/> or the operation is
        /// undeclared. When omitted, returns the value unchanged.
        /// </param>
        public EnumMaskConverter(
            TEnum mask,
            EnumMaskOperation operation = EnumMaskOperation.And,
            ConverterFallback<TEnum>? fallback = null)
        {
            _mask = mask;
            _operation = operation;
            _fallback = fallback ?? _fallback;
        }

        /// <summary>
        /// Applies the authored mask to the specified value.
        /// </summary>
        /// <param name="value">The value to combine with the mask.</param>
        /// <returns>The combined value, not necessarily a declared member, or the fallback for a non-flags enum or an undeclared operation.</returns>
        public TEnum Convert(TEnum value)
        {
            if (!EnumBits<TEnum>.IsFlags)
            {
                return _fallback.Fail(
                    converter: this,
                    value: value,
                    problem: $"{typeof(TEnum).Name} is not marked [Flags], so it has no flags to combine the mask with");
            }

            var comparer = EqualityComparer<TEnum>.Default;

            if (_hasCache
                && _cachedOperation == _operation
                && comparer.Equals(_cachedValue, value)
                && comparer.Equals(_cachedMask, _mask))
                return _cachedResult;

            var bits = EnumBits<TEnum>.BitsOf(value);
            var mask = EnumBits<TEnum>.BitsOf(_mask);

            ulong? combined = _operation switch
            {
                EnumMaskOperation.And => bits & mask,
                EnumMaskOperation.Or => bits | mask,
                EnumMaskOperation.Xor => bits ^ mask,
                EnumMaskOperation.Clear => bits & ~mask,
                _ => null
            };

            if (combined is null)
            {
                return _fallback.Fail(
                    converter: this,
                    value: value,
                    problem: $"the operation {_operation.Describe()} is not a declared {nameof(EnumMaskOperation)}");
            }

            _cachedResult = EnumBits<TEnum>.FromBits(combined.Value);
            _cachedOperation = _operation;
            _cachedValue = value;
            _cachedMask = _mask;
            _hasCache = true;

            return _cachedResult;
        }
    }
}
