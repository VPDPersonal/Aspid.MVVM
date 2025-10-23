using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(BoxCollider), "m_Center")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder - Center Switcher")]
    [AddComponentContextMenu(typeof(BoxCollider),"Add BoxCollider Binder/BoxCollider Binder - Center Switcher")]
    public sealed class BoxColliderCenterSwitcherMonoBinder : SwitcherMonoBinder<BoxCollider, Vector3>
    {
        [Header("Converters")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            CachedComponent.center = _converter.Convert(value, CachedComponent.center);
    }
}