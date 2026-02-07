using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Center EnumGroup")]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Center", SubPath = "EnumGroup")]
    public sealed class CapsuleColliderCenterEnumGroupMonoBinder : EnumGroupMonoBinder<CapsuleCollider>
    {
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [SerializeField] private Vector3CombineConverter _defaultValueConverter = Vector3CombineConverter.Default;
        [SerializeField] private Vector3CombineConverter _selectedValueConverter = Vector3CombineConverter.Default;
        
        protected override void SetDefaultValue(CapsuleCollider element) =>
            element.center = _defaultValueConverter.Convert(_defaultValue, element.center);

        protected override void SetSelectedValue(CapsuleCollider element) =>
            element.center = _selectedValueConverter.Convert(_defaultValue, element.center);
    }
}