using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="SpriteRenderer.size"/>.
    /// </summary>
    /// <remarks>
    /// Applies only when <see cref="SpriteRenderer.drawMode"/> is <see cref="SpriteDrawMode.Sliced"/> or
    /// <see cref="SpriteDrawMode.Tiled"/>. Negative and non-finite components become zero.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Size")]
    public class SpriteRendererSizeMonoBinder : ComponentMonoBinder<SpriteRenderer, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.size;
            set => CachedComponent.size = this.NonNegative(value);
        }
    }
}
