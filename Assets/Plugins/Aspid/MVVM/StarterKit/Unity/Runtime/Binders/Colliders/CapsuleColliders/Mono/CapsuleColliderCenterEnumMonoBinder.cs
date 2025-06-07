using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(CapsuleCollider), "m_Center")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder - Center Enum")]
    [AddComponentContextMenu(typeof(CapsuleCollider),"Add CapsuleCollider Binder/CapsuleCollider Binder - Center Enum")]
    public sealed class CapsuleColliderCenterEnumMonoBinder : EnumComponentMonoBinder<CapsuleCollider, Vector3>
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            CachedComponent.center = _converter.Convert(value, CachedComponent.center);
    }
}