using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{SpriteRenderer}"/> that binds <see cref="SpriteRenderer.size"/>.
    /// </summary>
    /// <remarks>
    /// The size a tiled or sliced sprite is stretched to — a health bar, a growing panel, a rope. It is the
    /// only property of the domain that was left out, and it is the one that needs a binder most: the
    /// alternative is scaling the transform, which stretches the border of a sliced sprite with it.
    /// <para/>
    /// Ignored by Unity unless <see cref="SpriteRenderer.drawMode"/> is
    /// <see cref="SpriteDrawMode.Sliced"/> or <see cref="SpriteDrawMode.Tiled"/>. Negative values are
    /// clamped to zero, which is also where a non-finite value lands.
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
