using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="CapsuleCollider.radius"/>.
    /// </summary>
    /// <remarks>
    /// A negative value is raised to zero.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Radius")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Radius")]
    public class CapsuleColliderRadiusMonoBinder : ComponentFloatMonoBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.radius;
            set => CachedComponent.radius = this.NonNegative(value);
        }
    }
}
