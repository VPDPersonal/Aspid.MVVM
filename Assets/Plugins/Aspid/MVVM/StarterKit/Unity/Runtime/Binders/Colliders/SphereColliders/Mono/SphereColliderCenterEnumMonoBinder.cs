using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(SphereCollider), "m_Center")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder - Center Enum")]
    [AddComponentContextMenu(typeof(SphereCollider),"Add SphereCollider Binder/SphereCollider Binder - Center Enum")]
    public sealed class SphereColliderCenterEnumMonoBinder : EnumMonoBinder<SphereCollider, Vector3>
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            CachedComponent.center = _converter.Convert(value, CachedComponent.center);
    }
}