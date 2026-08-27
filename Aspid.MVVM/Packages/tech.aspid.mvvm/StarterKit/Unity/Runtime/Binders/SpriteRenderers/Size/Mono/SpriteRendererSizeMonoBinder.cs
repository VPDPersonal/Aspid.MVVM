using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{SpriteRenderer}"/> that binds <see cref="SpriteRenderer.size"/>.
    /// </summary>
    /// <remarks>
    /// Ignored by Unity unless <see cref="SpriteRenderer.drawMode"/> is <see cref="SpriteDrawMode.Sliced"/> or
    /// <see cref="SpriteDrawMode.Tiled"/>. Negative and non-finite values are clamped to zero.
    /// </remarks>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Size")]
    public class SpriteRendererSizeMonoBinder : ComponentVector2MonoBinder<SpriteRenderer>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.size;
            set => CachedComponent.size = new Vector2(BinderMath.SafeClamp(value.x, 0f, float.MaxValue), BinderMath.SafeClamp(value.y, 0f, float.MaxValue));
        }
    }
}
