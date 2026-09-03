#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes one number into the chosen sides of a padding.
    /// </summary>
    /// <remarks>
    /// One <see cref="RectOffset"/> instance is rewritten on every push to avoid allocating, so the
    /// result must not be held onto.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Rect Offset",
        Name = "Int To Rect Offset",
        Tooltip = "Writes one number into the chosen sides of a padding")]
    public sealed class IntToRectOffsetConverter : IConverter<int, RectOffset>
    {
        [Tooltip("Which sides the number is written into.")]
        [SerializeField] private RectSides _sides = RectSides.All;

        [Tooltip("The values used for the sides the number does not write.")]
        [SerializeField] private RectOffset _base = new();

        [NonSerialized] private RectOffset? _result;

        [NonSerialized] private RectOffset? _emptyBase;

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
            var fallback = _base ?? (_emptyBase ??= new RectOffset());

            _result.left = (_sides & RectSides.Left) != 0 ? value : fallback.left;
            _result.right = (_sides & RectSides.Right) != 0 ? value : fallback.right;
            _result.top = (_sides & RectSides.Top) != 0 ? value : fallback.top;
            _result.bottom = (_sides & RectSides.Bottom) != 0 ? value : fallback.bottom;

            return _result;
        }
    }
}
