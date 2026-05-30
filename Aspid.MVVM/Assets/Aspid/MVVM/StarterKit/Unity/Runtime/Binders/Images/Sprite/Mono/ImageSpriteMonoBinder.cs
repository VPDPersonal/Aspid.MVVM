using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Image, Sprite}"/> that sets the <see cref="Image.sprite"/> property.
    /// Optionally disables the <see cref="Image"/> when the bound sprite is <see langword="null"/>.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current sprite value
    /// is sent back to the ViewModel.
    /// </remarks>
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
                CachedComponent.enabled = !_disabledWhenNull || value;
            }
        }

        /// <summary>
        /// Creates a <see cref="UnityEngine.Sprite"/> from <paramref name="value"/> and sets the <see cref="Image.sprite"/> property.
        /// </summary>
        /// <param name="value">The <see cref="Texture2D"/> to convert into a sprite, or <see langword="null"/> to clear the sprite.</param>
        [BinderLog]
        public void SetValue(Texture2D value)
        {
            if (_createdSprite) Object.Destroy(_createdSprite);

            _createdSprite = !value
                ? null
                : Sprite.Create(value, new Rect(0, 0, value.width, value.height), new Vector2(0.5f, 0.5f));

            SetValue(_createdSprite);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (_createdSprite) Object.Destroy(_createdSprite);
            _createdSprite = null;
        }
    }
}