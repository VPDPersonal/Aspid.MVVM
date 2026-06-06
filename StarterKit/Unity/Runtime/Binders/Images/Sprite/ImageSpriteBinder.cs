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

        private Sprite? _createdSprite;

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
            _createdSprite = CreateSprite(_createdSprite, value);
            SetValue(_createdSprite);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (_createdSprite) UnityEngine.Object.Destroy(_createdSprite);
            _createdSprite = null;
            Target.sprite = null;
        }

        /// <summary>
        /// Destroys the previously created <paramref name="oldSprite"/> and creates a new
        /// <see cref="UnityEngine.Sprite"/> from <paramref name="texture"/>.
        /// </summary>
        /// <param name="oldSprite">The previously created sprite to destroy, or <see langword="null"/>.</param>
        /// <param name="texture">The <see cref="Texture2D"/> to convert into a sprite, or <see langword="null"/> to create no sprite.</param>
        /// <returns>The newly created <see cref="UnityEngine.Sprite"/>, or <see langword="null"/> when <paramref name="texture"/> is <see langword="null"/>.</returns>
        internal static Sprite? CreateSprite(Sprite? oldSprite, Texture2D? texture)
        {
            if (oldSprite) UnityEngine.Object.Destroy(oldSprite);

            return !texture
                ? null
                : Sprite.Create(texture, new Rect(0, 0, texture!.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}