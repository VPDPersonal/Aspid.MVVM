using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{SpriteRenderer}"/> that binds <see cref="SpriteRenderer.flipX"/>.
    /// </summary>
    /// <remarks>
    /// Mirroring a sprite is how a 2D character faces the other way; it needed a scale binder before.
    /// </remarks>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_FlipX")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Flip X")]
    public class SpriteRendererFlipXMonoBinder : ComponentBoolMonoBinder<SpriteRenderer>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.flipX;
            set => CachedComponent.flipX = value;
        }
    }
}
