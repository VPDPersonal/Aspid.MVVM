using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds <see cref="SpriteRenderer.flipX"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_FlipX")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Flip X")]
    public class SpriteRendererFlipXMonoBinder : ComponentMonoBinder<SpriteRenderer, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.flipX;
            set => CachedComponent.flipX = value;
        }
    }
}
