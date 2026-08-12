using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder<SpriteRenderer>"/> that binds <see cref="SpriteRenderer.flipY"/>.
    /// </summary>
    /// <remarks>
    /// Mirrors the sprite vertically.
    /// </remarks>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_FlipY")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Flip Y")]
    public class SpriteRendererFlipYMonoBinder : ComponentBoolMonoBinder<SpriteRenderer>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.flipY;
            set => CachedComponent.flipY = value;
        }
    }
}
