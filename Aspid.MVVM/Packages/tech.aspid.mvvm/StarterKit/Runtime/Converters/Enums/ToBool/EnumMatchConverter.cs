using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Tests an enum value against an authored one.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being tested.</typeparam>
    /// <remarks>
    /// The flag tests are pure bit math: on an enum not marked <see cref="FlagsAttribute"/> they
    /// compare bit patterns nobody authored as flags.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Enum/To Bool",
        Name = "Match",
        Tooltip = "Tests an enum value against an authored one")]
    public class EnumMatchConverter<TEnum> : IConverter<TEnum, bool>
        where TEnum : struct, Enum
    {
        [Tooltip("The enum value the bound one is tested against.")]
        [SerializeField] private TEnum _target;

        [Tooltip("How the bound value is tested against the target.")]
        [SerializeField] private EnumMatch _match;

        [Tooltip("Invert the result.")]
        [SerializeField] private bool _isInvert;

        [Tooltip("Returned, without inverting, when the match is undeclared.")]
        [SerializeField] private bool _fallback;

        [NonSerialized] private ulong _targetBits;
        [NonSerialized] private TEnum _cachedTarget;
        [NonSerialized] private bool _hasTargetBits;

        /// <remarks>Default: testing equality against the enum's default value.</remarks>
        public EnumMatchConverter() { }

        /// <param name="target">The enum value the bound one is tested against.</param>
        /// <param name="match">How the bound value is tested against <paramref name="target"/>.</param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        /// <param name="fallback">
        /// Returned, without inverting, when the match is undeclared. When omitted, <see langword="false"/>.
        /// </param>
        public EnumMatchConverter(
            TEnum target,
            EnumMatch match = EnumMatch.Equal,
            bool isInvert = false,
            bool fallback = false)
        {
            _match = match;
            _target = target;
            _isInvert = isInvert;
            _fallback = fallback;
        }

        /// <summary>
        /// Tests the specified enum value against zthe authored one.
        /// </summary>
        /// <param name="value">The enum value to test.</param>
        /// <returns>
        /// The result, inverted when configured; an undeclared <see cref="EnumMatch"/> returns the
        /// fallback without inverting it.
        /// </returns>
        public bool Convert(TEnum value)
        {
            var target = TargetBits();
            var actual = EnumBits<TEnum>.BitsOf(value);

            // Testing the bits directly rather than through Enum.HasFlag skips Enum's type check
            // on every push.
            bool? matched = _match switch
            {
                EnumMatch.Equal => actual == target,
                EnumMatch.NotEquals => actual != target,
                EnumMatch.HasAllFlags => (actual & target) == target,
                EnumMatch.HasAnyFlag => (actual & target) != 0,
                _ => null
            };

            // Inverting an answer no test produced would turn the report into a true, so the
            // fallback is returned without inverting it.
            if (matched is null)
            {
                return this.UseFallback(
                    fallback: _fallback,
                    problem: $"the match {_match.Describe()} is not a declared {nameof(EnumMatch)}");
            }

            return _isInvert
                ? !matched.Value
                : matched.Value;
        }

        // BitsOf boxes, so the target's bits are cached; the target is serialized, so the Inspector
        // can change it between two pushes.
        private ulong TargetBits()
        {
            if (_hasTargetBits && EqualityComparer<TEnum>.Default.Equals(_cachedTarget, _target))
                return _targetBits;

            _hasTargetBits = true;
            _cachedTarget = _target;
            _targetBits = EnumBits<TEnum>.BitsOf(_target);

            return _targetBits;
        }
    }
}
