#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Image, Sprite}"/> that sets the <see cref="Image.sprite"/> property.
    /// Optionally disables the <see cref="Image"/> when the bound sprite is <see langword="null"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-Image-Sprite-1.1.0.xml" path="doc//member[@name='ImageSpriteBinder']/*" />
    [Serializable]
    public class ImageSpriteBinder : TargetBinder<Image, Sprite>, IBinder<Texture2D?>
    {
        [Tooltip("When enabled, disables the Image component when the bound sprite is null.")]
        [SerializeField] private bool _disabledWhenNull;

        protected sealed override Sprite? Property
        {
            get => Target.sprite;
            set
            {
                Target.sprite = value;
                Target.enabled = !_disabledWhenNull || value;
            }
        }

        /// <inheritdoc/>
        public ImageSpriteBinder(Image target, bool disabledWhenNull = true, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _disabledWhenNull = disabledWhenNull;
        }

        /// <summary>
        /// Creates a <see cref="UnityEngine.Sprite"/> from <paramref name="value"/> and sets the <see cref="Image.sprite"/> property.
        /// </summary>
        /// <param name="value">The <see cref="Texture2D"/> to convert into a sprite, or <see langword="null"/> to clear the sprite.</param>
        public void SetValue(Texture2D? value)
        {
            var sprite = !value
                ? null
                : Sprite.Create(value, new Rect(0, 0, value!.width, value.height), new Vector2(0.5f, 0.5f));

            SetValue(sprite);
        }
    }
}