using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(CapsuleCollider), "m_Center")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder - Center Switcher")]
    [AddComponentContextMenu(typeof(CapsuleCollider),"Add CapsuleCollider Binder/CapsuleCollider Binder - Center Switcher")]
    public sealed class CapsuleColliderCenterSwitcherMonoBinder : SwitcherMonoBinder<CapsuleCollider, Vector3>
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            CachedComponent.center = _converter.Convert(value, CachedComponent.center);
    }
}