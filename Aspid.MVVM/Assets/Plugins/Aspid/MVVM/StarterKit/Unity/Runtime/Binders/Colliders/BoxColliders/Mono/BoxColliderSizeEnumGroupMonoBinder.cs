using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Size EnumGroup")]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Size", SubPath = "EnumGroup")]
    public sealed class BoxColliderSizeEnumGroupMonoBinder : EnumGroupMonoBinder<BoxCollider>
    {
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [SerializeField] private Vector3CombineConverter _defaultValueConverter = Vector3CombineConverter.Default;
        [SerializeField] private Vector3CombineConverter _selectedValueConverter = Vector3CombineConverter.Default;
        
        protected override void SetDefaultValue(BoxCollider element) =>
            element.size = _defaultValueConverter.Convert(_defaultValue, element.center);

        protected override void SetSelectedValue(BoxCollider element) =>
            element.size = _selectedValueConverter.Convert(_defaultValue, element.center);
    }
}