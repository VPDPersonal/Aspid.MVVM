using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Tests an enum value against an authored one.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being tested.</typeparam>
    /// <remarks>
    /// "Show this panel while the state is Loading" needed a boolean property per state on the
    /// ViewModel, one for every state any View cared about.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Enum", Name = "Enum To Bool", Tooltip = "Tests an enum value against an authored one")]
    public sealed class EnumToBoolConverter<TEnum> : IConverter<TEnum, bool>
        where TEnum : struct, Enum
    {
        [Tooltip("The enum value the bound one is tested against.")]
        [SerializeField] private TEnum _target;

        [Tooltip("How the bound value is tested against the target.")]
        [SerializeField] private EnumMatch _match;

        [Tooltip("Invert the result.")]
        [SerializeField] private bool _isInvert;

        public EnumToBoolConverter() { }

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

        // Reading the bits directly rather than calling Enum.HasFlag skips Enum's type check on
        // every push. It is not allocation-free — Convert.ToInt64 has no generic overload, so it
        // binds to the object one and boxes, the same as HasFlag. EnumMembers avoids that by
        // converting once per member at type load; this path has no cache to hang it on.
        private static long EnumNumber(TEnum value) => System.Convert.ToInt64(value);
    }
}
