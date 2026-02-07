using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Center Switcher")]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Center", SubPath = "Switcher")]
    public sealed class CapsuleColliderCenterSwitcherMonoBinder : SwitcherMonoBinder<CapsuleCollider, Vector3>
    {
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            CachedComponent.center = _converter.Convert(value, CachedComponent.center);
    }
}