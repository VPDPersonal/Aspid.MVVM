#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a <see cref="Texture2D"/> in a <see cref="Sprite"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="Sprite.Create(Texture2D, Rect, Vector2)"/> allocates every time it is called, and a
    /// binder pushes on every notification rather than on every change — so the result is cached
    /// against the texture it came from. Without that, a bound avatar leaks a sprite per frame.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Texture", Name = "Texture2 D To Sprite", Tooltip = "Wraps a  in a ")]
    public sealed class Texture2DToSpriteConverter : IConverter<Texture2D?, Sprite?>
    {
        [Tooltip("Where the sprite's pivot sits, in normalised coordinates.")]
        [SerializeField] private Vector2 _pivot = new(0.5f, 0.5f);

        [Tooltip("How many texture pixels make up one world unit.")]
        [SerializeField] private float _pixelsPerUnit = 100f;

        [NonSerialized] private Texture2D? _lastTexture;
        [NonSerialized] private Sprite? _lastSprite;

        /// <remarks>Default: with a centred pivot.</remarks>
        public Texture2DToSpriteConverter() { }

        /// <param name="pivot">Where the sprite's pivot sits, in normalised coordinates.</param>
        /// <param name="pixelsPerUnit">How many texture pixels make up one world unit.</param>
        public Texture2DToSpriteConverter(Vector2 pivot, float pixelsPerUnit = 100f)
        {
            _pivot = pivot;
            _pixelsPerUnit = pixelsPerUnit;
        }

        /// <summary>
        /// Wraps the specified texture in a sprite.
        /// </summary>
        /// <param name="value">The texture to wrap.</param>
        /// <returns>
        /// A sprite covering the whole texture, reused while the texture is unchanged, or
        /// <see langword="null"/> when the texture is missing.
        /// </returns>
        public Sprite? Convert(Texture2D? value)
        {
            if (value == null)
            {
                _lastTexture = null;
                _lastSprite = null;
                return null;
            }

            if (ReferenceEquals(_lastTexture, value) && _lastSprite != null) return _lastSprite;

            _lastTexture = value;
            _lastSprite = Sprite.Create(
                value,
                new Rect(0f, 0f, value.width, value.height),
                _pivot,
                _pixelsPerUnit <= 0f ? 100f : _pixelsPerUnit);

            return _lastSprite;
        }
    }
}
