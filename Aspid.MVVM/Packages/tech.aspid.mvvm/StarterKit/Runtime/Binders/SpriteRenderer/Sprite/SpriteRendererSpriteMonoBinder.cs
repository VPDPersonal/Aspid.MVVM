using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="SpriteRenderer.sprite"/>.
    /// </summary>
    /// <remarks>
    /// A <see cref="Texture2D"/> is wrapped in a sprite owned by the binder and destroyed on unbind.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Sprite")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sprite")]
    public partial class SpriteRendererSpriteMonoBinder : ComponentMonoBinder<SpriteRenderer, Sprite>,
        IBinder<Texture2D>
    {
        private Sprite _createdSprite;

        /// <inheritdoc/>
        protected sealed override Sprite Property
        {
            get => CachedComponent.sprite;
            set => CachedComponent.sprite = value;
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
                if (CachedComponent.sprite == _createdSprite) CachedComponent.sprite = null;
                
                // ReSharper disable once ArrangeStaticMemberQualifier
                Object.Destroy(_createdSprite);
            }

            _createdSprite = null;
            base.OnUnbound();
        }
    }
}
