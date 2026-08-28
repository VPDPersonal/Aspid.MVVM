using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{SpriteRenderer, Vector2}"/> that binds <see cref="SpriteRenderer.size"/>.
    /// </summary>
    /// <remarks>
    /// Ignored by Unity unless <see cref="SpriteRenderer.drawMode"/> is <see cref="SpriteDrawMode.Sliced"/> or
    /// <see cref="SpriteDrawMode.Tiled"/>. Negative and non-finite values are clamped to zero.
    /// </remarks>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Size")]
    public class SpriteRendererSizeMonoBinder : ComponentMonoBinder<SpriteRenderer, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.size;
            set => CachedComponent.size = new Vector2(this.SafeClamp(value.x, 0f, float.MaxValue), this.SafeClamp(value.y, 0f, float.MaxValue));
        }
    }
}
