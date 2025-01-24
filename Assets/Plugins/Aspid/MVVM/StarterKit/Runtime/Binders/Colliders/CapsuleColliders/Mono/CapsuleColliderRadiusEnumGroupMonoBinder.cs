using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder - Radius EnumGroup")]
    public sealed class CapsuleColliderRadiusEnumGroupMonoBinder : EnumGroupMonoBinder<CapsuleCollider>
    {
        [Header("Parameters")]
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _selectedValue;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
        protected override void SetDefaultValue(CapsuleCollider element) =>
            element.radius = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(CapsuleCollider element) =>
            element.radius =  _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}