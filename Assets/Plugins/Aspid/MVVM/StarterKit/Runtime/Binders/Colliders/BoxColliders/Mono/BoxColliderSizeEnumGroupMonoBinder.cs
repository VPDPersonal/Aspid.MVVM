using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Collider/BoxCollider Binder - Size EnumGroup")]
    public sealed class BoxColliderSizeEnumGroupMonoBinder : EnumGroupMonoBinder<BoxCollider>
    {
        [Header("Parameters")]
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