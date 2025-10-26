using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(BoxCollider), "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder - Size EnumGroup")]
    [AddComponentContextMenu(typeof(BoxCollider),"Add BoxCollider Binder/BoxCollider Binder - Size EnumGroup")]
    public sealed class BoxColliderSizeEnumGroupMonoBinder : EnumGroupMonoBinder<BoxCollider>
    {
        [Header("Values")]
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        
        [Header("Converters")]
        [SerializeField] private Vector3CombineConverter _defaultValueConverter = Vector3CombineConverter.Default;
        [SerializeField] private Vector3CombineConverter _selectedValueConverter = Vector3CombineConverter.Default;
        
        protected override void SetDefaultValue(BoxCollider element) =>
            element.size = _defaultValueConverter.Convert(_defaultValue, element.center);

        protected override void SetSelectedValue(BoxCollider element) =>
            element.size = _selectedValueConverter.Convert(_defaultValue, element.center);
    }
}