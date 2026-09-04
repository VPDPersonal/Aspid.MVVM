using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Image.sprite"/>, also from a
    /// <see cref="Texture2D"/>.
    /// </summary>
    /// <remarks>
    /// A texture is wrapped in a sprite owned by the binder and destroyed on unbind. Optionally disables the
    /// <see cref="Image"/> while the sprite is <see langword="null"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite")]
    public partial class ImageSpriteMonoBinder : ComponentMonoBinder<Image, Sprite>, IBinder<Texture2D>
    {
        [Tooltip("Disable the Image while the sprite is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        private Sprite _createdSprite;

        /// <inheritdoc/>
        protected sealed override Sprite Property
        {
            get => CachedComponent.sprite;
            set => CachedComponent.SetSprite(value, _disabledWhenNull);
        }

        /// <summary>
        /// Shows a sprite created from the texture; <see langword="null"/> clears the sprite.
        /// </summary>
        /// <param name="value">The texture received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Texture2D value)
        {
            _createdSprite = SpriteBinderHelper.CreateSprite(_createdSprite, value);
            SetValue(_createdSprite);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (_createdSprite)
            {
                if (CachedComponent.sprite == _createdSprite) Property = null;
                
                // ReSharper disable once ArrangeStaticMemberQualifier
                Object.Destroy(_createdSprite);
            }

            _createdSprite = null;
            base.OnUnbound();
        }
    }
}
