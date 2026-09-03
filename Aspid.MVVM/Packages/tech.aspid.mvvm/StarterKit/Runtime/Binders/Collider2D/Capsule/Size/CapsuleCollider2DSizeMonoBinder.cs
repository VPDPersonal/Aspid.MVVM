using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="CapsuleCollider2D.size"/>.
    /// </summary>
    /// <remarks>
    /// Negative components are raised to zero.
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
            set => CachedComponent.size = this.NonNegative(value);
        }
    }
}
