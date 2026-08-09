#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes one number into the chosen sides of a padding.
    /// </summary>
    /// <remarks>
    /// <see cref="RectOffset"/> is a class, so returning a new one on every push would allocate once
    /// per notification. One instance is kept and rewritten instead — safe because layout reads the
    /// values immediately, but the result must not be held onto.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Layout", Name = "Int To Rect Offset", Tooltip = "Writes one number into the chosen sides of a padding")]
    public sealed class IntToRectOffsetConverter : IConverter<int, RectOffset>
    {
        [Tooltip("Which sides the number is written into.")]
        [SerializeField] private RectSides _sides = RectSides.All;

        [Tooltip("The values used for the sides the number does not write.")]
        [SerializeField] private RectOffset _base = new();

        [NonSerialized] private RectOffset? _result;

        /// <remarks>Default: writing every side.</remarks>
        public IntToRectOffsetConverter() { }

        /// <param name="sides">Which sides the number is written into.</param>
        public IntToRectOffsetConverter(RectSides sides)
        {
            _sides = sides;
        }

        /// <summary>
        /// Writes the specified number into the chosen sides.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>
        /// The padding. The same instance is returned every call, so copy it if it must outlive the
        /// next push.
        /// </returns>
        public RectOffset Convert(int value)
        {
            _result ??= new RectOffset();
            var fallback = _base ?? new RectOffset();

            _result.left = _sides.HasFlag(RectSides.Left) ? value : fallback.left;
            _result.right = _sides.HasFlag(RectSides.Right) ? value : fallback.right;
            _result.top = _sides.HasFlag(RectSides.Top) ? value : fallback.top;
            _result.bottom = _sides.HasFlag(RectSides.Bottom) ? value : fallback.bottom;

            return _result;
        }
    }
}
