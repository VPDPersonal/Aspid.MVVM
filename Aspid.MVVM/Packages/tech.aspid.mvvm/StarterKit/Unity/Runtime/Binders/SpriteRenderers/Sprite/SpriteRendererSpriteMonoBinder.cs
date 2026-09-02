using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds <see cref="SpriteRenderer.sprite"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Sprite")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sprite")]
    public partial class SpriteRendererSpriteMonoBinder : ComponentMonoBinder<SpriteRenderer, Sprite>, IBinder<Texture2D>
    {
        private Sprite _createdSprite;

        /// <inheritdoc/>
        protected sealed override Sprite Property
        {
            get => CachedComponent.sprite;
            set => CachedComponent.sprite = value;
        }

        /// <summary>
        /// Creates a <see cref="UnityEngine.Sprite"/> from <paramref name="value"/> and sets the <see cref="SpriteRenderer.sprite"/> property.
        /// </summary>
        /// <param name="value">The <see cref="Texture2D"/> to convert into a sprite, or <see langword="null"/> to clear the sprite.</param>
        [BinderLog]
        public void SetValue(Texture2D value)
        {
            _createdSprite = SpriteBinderHelper.CreateSprite(_createdSprite, value);
            SetValue(_createdSprite);
        }

        /// <summary>
        /// Destroys the sprite this binder created and then runs the base implementation.
        /// </summary>
        /// <remarks>
        /// The renderer is cleared only while it still shows the created sprite; a sprite assigned directly stays.
        /// </remarks>
        protected override void OnUnbound()
        {
            if (_createdSprite)
            {
                if (CachedComponent.sprite == _createdSprite) CachedComponent.sprite = null;
                Object.Destroy(_createdSprite);
            }

            _createdSprite = null;
            base.OnUnbound();
        }
    }
}
