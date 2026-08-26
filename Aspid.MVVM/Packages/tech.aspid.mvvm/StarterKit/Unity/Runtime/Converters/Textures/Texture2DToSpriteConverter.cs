#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a <see cref="Texture2D"/> in a <see cref="Sprite"/>.
    /// </summary>
    /// <remarks>
    /// The sprite is owned by the converter: it is cached against its texture, since
    /// <see cref="Sprite.Create(Texture2D, Rect, Vector2)"/> allocates and a binder pushes on every
    /// notification, and destroyed once the texture changes.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Texture",
        Name = "Texture2D To Sprite",
        Tooltip = "Wraps a Texture2D in a Sprite")]
    public sealed class Texture2DToSpriteConverter : IConverter<Texture2D?, Sprite?>
    {
        [Tooltip("Where the sprite's pivot sits, in normalized coordinates.")]
        [SerializeField] private Vector2 _pivot = new(0.5f, 0.5f);

        [Tooltip("How many texture pixels make up one world unit.")]
        [SerializeField] [Min(0)] private float _pixelsPerUnit = 100f;

        [NonSerialized] private Texture2D? _lastTexture;
        [NonSerialized] private Sprite? _lastSprite;

        /// <remarks>Default: a centered pivot at 100 pixels per unit, the same as a fresh sprite import.</remarks>
        public Texture2DToSpriteConverter() { }

        /// <param name="pivot">Where the sprite's pivot sits, in normalized coordinates.</param>
        /// <param name="pixelsPerUnit">
        /// How many texture pixels make up one world unit. A value that is not above zero is reported
        /// as an error and 100 is used instead.
        /// </param>
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
        /// <see langword="null"/> when the texture is missing or destroyed. The previously returned
        /// sprite is destroyed on the way, so a caller holding on to it is left with nothing.
        /// </returns>
        public Sprite? Convert(Texture2D? value)
        {
            if (value == null)
            {
                Release();
                return null;
            }

            if (ReferenceEquals(_lastTexture, value) && _lastSprite != null)
                return _lastSprite;

            Release();

            _lastTexture = value;
            _lastSprite = Sprite.Create(
                texture: value,
                rect: new Rect(0f, 0f, value.width, value.height),
                pivot: _pivot,
                pixelsPerUnit: PixelsPerUnit());

            return _lastSprite;
        }

        // Testing the good case rather than the bad one puts NaN in the report along with zero.
        private float PixelsPerUnit()
        {
            if (_pixelsPerUnit > 0f)
                return _pixelsPerUnit;

            this.LogError(
                problem: $"pixels per unit is {_pixelsPerUnit.Describe()}, which is not a scale",
                consequence: "Building the sprite at 100 pixels per unit instead.");

            return 100f;
        }

        private void Release()
        {
            if (_lastSprite != null)
                Object.Destroy(_lastSprite);

            _lastSprite = null;
            _lastTexture = null;
        }
    }
}
