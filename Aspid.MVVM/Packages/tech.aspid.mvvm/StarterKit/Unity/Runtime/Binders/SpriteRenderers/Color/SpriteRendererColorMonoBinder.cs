using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{SpriteRenderer, Color}"/> that binds <see cref="SpriteRenderer.color"/>.
    /// </summary>
    /// <remarks>
    /// Tints the sprite directly, without touching the shared material the way the renderer colour binders do.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Color")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Color")]
    public class SpriteRendererColorMonoBinder : ComponentMonoBinder<SpriteRenderer, Color>, IColorBinder
    {
        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => CachedComponent.color;
            set => CachedComponent.color = value;
        }
    }
}
