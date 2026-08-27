using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{CapsuleCollider}"/> that binds <see cref="CapsuleCollider.height"/>.
    /// </summary>
    /// <remarks>
    /// Clamped non-negative; a non-finite value maps to <c>0</c>.
    /// </remarks>
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Height")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Height")]
    public class CapsuleColliderHeightMonoBinder : ComponentFloatMonoBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.height;
            set => CachedComponent.height = BinderMath.SafeClamp(value, 0f, float.MaxValue);
        }
    }
}
