using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{CapsuleCollider}"/> that binds <see cref="CapsuleCollider.direction"/>.
    /// </summary>
    /// <remarks>
    /// Which axis the capsule stands on: 0 for X, 1 for Y, 2 for Z. A character that lies down changes it, and
    /// so does a projectile that turns. Clamped to the three axes that exist — Unity accepts any integer and
    /// then behaves as if it were zero.
    /// </remarks>
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Direction")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Direction")]
    public class CapsuleColliderDirectionMonoBinder : ComponentIntMonoBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.direction;
            set => CachedComponent.direction = Mathf.Clamp(value, 0, 2);
        }
    }
}
