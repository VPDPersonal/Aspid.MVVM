using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Center Switcher")]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Center", SubPath = "Switcher")]
    public sealed class BoxColliderCenterSwitcherMonoBinder : SwitcherMonoBinder<BoxCollider, Vector3>
    {
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            CachedComponent.center = _converter.Convert(value, CachedComponent.center);
    }
}