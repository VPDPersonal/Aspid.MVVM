using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{CapsuleCollider2D, Vector2}"/> that binds <see cref="CapsuleCollider2D.size"/>.
    /// </summary>
    /// <remarks>
    /// Clamped non-negative on both axes; a non-finite value maps to <c>0</c>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CapsuleCollider2D), serializePropertyNames: "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider2D/Capsule/CapsuleCollider2D Binder – Size")]
    public class CapsuleCollider2DSizeMonoBinder : ComponentMonoBinder<CapsuleCollider2D, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.size;
            set => CachedComponent.size = new Vector2(this.SafeClamp(value.x, 0f, float.MaxValue), this.SafeClamp(value.y, 0f, float.MaxValue));
        }
    }
}
