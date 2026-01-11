using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Center")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Center EnumGroup")]
    public sealed class BoxColliderCenterEnumGroupMonoBinder : EnumGroupMonoBinder<BoxCollider>
    {
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [SerializeField] private Vector3CombineConverter _defaultValueConverter = Vector3CombineConverter.Default;
        [SerializeField] private Vector3CombineConverter _selectedValueConverter = Vector3CombineConverter.Default;
        
        protected override void SetDefaultValue(BoxCollider element) =>
            element.center = _defaultValueConverter.Convert(_defaultValue, element.center);

        protected override void SetSelectedValue(BoxCollider element) =>
            element.center = _selectedValueConverter.Convert(_defaultValue, element.center);
    }
}