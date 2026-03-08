using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="CapsuleCollider.radius"/> property on a <see cref="CapsuleCollider"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
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