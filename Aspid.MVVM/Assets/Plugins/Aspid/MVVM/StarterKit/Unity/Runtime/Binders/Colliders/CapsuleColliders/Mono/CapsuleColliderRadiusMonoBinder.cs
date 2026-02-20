using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Radius")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Radius")]
    public class CapsuleColliderRadiusMonoBinder : ComponentFloatMonoBinder<CapsuleCollider>
    {
        protected sealed override float Property
        {
            get => CachedComponent.radius;
            set => CachedComponent.radius = value;
        }
    }
}