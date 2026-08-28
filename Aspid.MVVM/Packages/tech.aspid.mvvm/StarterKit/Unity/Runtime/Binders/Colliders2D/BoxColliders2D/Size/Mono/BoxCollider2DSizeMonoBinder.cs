using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{BoxCollider2D, Vector2}"/> that binds <see cref="BoxCollider2D.size"/>.
    /// </summary>
    /// <remarks>
    /// Clamped non-negative on both axes; a non-finite value maps to <c>0</c>.
    /// </remarks>
    [AddBinderContextMenu(typeof(BoxCollider2D), serializePropertyNames: "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Box/BoxCollider2D Binder – Size")]
    public class BoxCollider2DSizeMonoBinder : ComponentMonoBinder<BoxCollider2D, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.size;
            set => CachedComponent.size = new Vector2(this.SafeClamp(value.x, 0f, float.MaxValue), this.SafeClamp(value.y, 0f, float.MaxValue));
        }
    }
}
