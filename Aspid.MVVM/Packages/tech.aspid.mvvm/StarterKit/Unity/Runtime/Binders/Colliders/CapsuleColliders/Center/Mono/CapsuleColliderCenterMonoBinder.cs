using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector3MonoBinder{CapsuleCollider}"/> that binds the <see cref="CapsuleCollider.center"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Center")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Center")]
    public class CapsuleColliderCenterMonoBinder : ComponentVector3MonoBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.center;
            set => CachedComponent.center = value;
        }
    }
}