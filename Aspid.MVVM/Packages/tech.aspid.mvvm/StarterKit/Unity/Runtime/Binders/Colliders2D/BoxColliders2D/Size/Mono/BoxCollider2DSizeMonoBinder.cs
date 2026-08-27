using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{BoxCollider2D}"/> that binds <see cref="BoxCollider2D.size"/>.
    /// </summary>
    /// <remarks>
    /// Clamped non-negative on both axes; a non-finite value maps to <c>0</c>.
    /// </remarks>
    [AddBinderContextMenu(typeof(BoxCollider2D), serializePropertyNames: "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Box/BoxCollider2D Binder – Size")]
    public class BoxCollider2DSizeMonoBinder : ComponentVector2MonoBinder<BoxCollider2D>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.size;
            set => CachedComponent.size = new Vector2(BinderMath.SafeClamp(value.x, 0f, float.MaxValue), BinderMath.SafeClamp(value.y, 0f, float.MaxValue));
        }
    }
}
