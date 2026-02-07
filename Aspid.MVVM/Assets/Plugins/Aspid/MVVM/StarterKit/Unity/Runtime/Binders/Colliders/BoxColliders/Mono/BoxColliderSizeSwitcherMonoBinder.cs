using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Size Switcher")]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Size", SubPath = "Switcher")]
    public sealed class BoxColliderSizeSwitcherMonoBinder : SwitcherMonoBinder<BoxCollider, Vector3>
    {
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            CachedComponent.size = _converter.Convert(value, CachedComponent.size);
    }
}