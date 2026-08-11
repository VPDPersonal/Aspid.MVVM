using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{CapsuleCollider}"/> that binds <see cref="CapsuleCollider.height"/>.
    /// </summary>
    /// <remarks>
    /// The domain had the capsule's radius and not its height, which is the half of it a crouch, a stretch or a
    /// growing character changes. Clamped non-negative: a negative height leaves the collider inverted, and a
    /// non-finite one lands on zero rather than reaching the physics engine.
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
