using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{SpriteRenderer, Sprite}"/> that binds <see cref="SpriteRenderer.sprite"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Sprite")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sprite")]
    public class SpriteRendererSpriteMonoBinder : ComponentMonoBinder<SpriteRenderer, Sprite>
    {
        /// <inheritdoc/>
        protected sealed override Sprite Property
        {
            get => CachedComponent.sprite;
            set => CachedComponent.sprite = value;
        }
    }
}
