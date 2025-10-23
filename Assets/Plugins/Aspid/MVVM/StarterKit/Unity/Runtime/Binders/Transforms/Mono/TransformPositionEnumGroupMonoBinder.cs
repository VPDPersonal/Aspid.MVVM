using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Transform), "m_LocalPosition")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Position EnumGroup")]
    [AddComponentContextMenu(typeof(Transform),"Add Transform Binder/Transform Binder - Position EnumGroup")]
    public sealed class TransformPositionEnumGroupMonoBinder : EnumGroupMonoBinder<Transform>
    {
        [Header("Values")]
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;
        [SerializeField] private Space _space = Space.World;    
        
        [Header("Converters")]
        [SerializeField] private Vector3CombineConverter _defaultValueConverter = Vector3CombineConverter.Default;
        [SerializeField] private Vector3CombineConverter _selectedValueConverter = Vector3CombineConverter.Default;

        protected override void SetDefaultValue(Transform element) =>
            element.SetPosition(_defaultValue, _space, _defaultValueConverter);

        protected override void SetSelectedValue(Transform element) =>
            element.SetPosition(_selectedValue, _space, _selectedValueConverter);
    }
}