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
                if (_disabledWhenNull) Target.enabled = value;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
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
            _createdSprite = SpriteBinderHelper.CreateSprite(_createdSprite, value);
            SetValue(_createdSprite);
        }

        /// <summary>
        /// Called after unbinding. Destroys the sprite this binder created, clears the image, and then runs the
        /// base implementation.
        /// </summary>
        /// <remarks>
        /// The destruction is the part worth knowing: a sprite built here from a bound texture belongs to this
        /// binder and nothing else would ever free it. A sprite assigned from the ViewModel is not touched — only
        /// the one this binder made.
        /// </remarks>
        protected override void OnUnbound()
        {
            if (_createdSprite) UnityEngine.Object.Destroy(_createdSprite);
            _createdSprite = null;
            Target.sprite = null;
            base.OnUnbound();
        }
    }
}
