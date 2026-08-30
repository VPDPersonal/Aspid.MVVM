using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that sets the <see cref="Image.sprite"/> property.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite")]
    public partial class ImageSpriteMonoBinder : ComponentMonoBinder<Image, Sprite>, IBinder<Texture2D>
    {
        [Tooltip("When enabled, disables the Image component when the bound sprite is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        private Sprite _createdSprite;

        protected sealed override Sprite Property
        {
            get => CachedComponent.sprite;
            set
            {
                CachedComponent.sprite = value;
                if (_disabledWhenNull) CachedComponent.enabled = value;
            }
        }

        /// <summary>
        /// Creates a <see cref="UnityEngine.Sprite"/> from <paramref name="value"/> and sets the <see cref="Image.sprite"/> property.
        /// </summary>
        /// <param name="value">The <see cref="Texture2D"/> to convert into a sprite, or <see langword="null"/> to clear the sprite.</param>
        [BinderLog]
        public void SetValue(Texture2D value)
        {
            _createdSprite = SpriteBinderHelper.CreateSprite(_createdSprite, value);
            SetValue(_createdSprite);
        }

        /// <summary>
        /// Destroys the sprite this binder created, clears the sprite, and then runs the base implementation.
        /// </summary>
        /// <remarks>Only a sprite created from a bound <see cref="Texture2D"/> is destroyed; one assigned directly is left untouched.</remarks>
        protected override void OnUnbound()
        {
            if (_createdSprite) Object.Destroy(_createdSprite);
            _createdSprite = null;
            CachedComponent.sprite = null;
            base.OnUnbound();
        }
    }
}
