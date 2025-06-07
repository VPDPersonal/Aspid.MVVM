using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(BoxCollider), "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder - Size Switcher")]
    [AddComponentContextMenu(typeof(BoxCollider),"Add BoxCollider Binder/BoxCollider Binder - Size Switcher")]
    public sealed class BoxColliderSizeSwitcherMonoBinder : SwitcherMonoBinder<BoxCollider, Vector3>
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            CachedComponent.size = _converter.Convert(value, CachedComponent.size);
    }
}