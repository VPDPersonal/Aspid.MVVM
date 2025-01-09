using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/CapsuleCollider Binder - Radius EnumGroup")]
    public sealed class CapsuleColliderRadiusEnumGroupMonoBinder : EnumGroupMonoBinder<CapsuleCollider>
    {
        [Header("Parameters")]
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _selectedValue;
        
        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _defaultValueConverter;
#else
        private IConverterFloat _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _selectedValueConverter;
#else
        private IConverterFloat _selectedValueConverter;
#endif
        
        protected override void SetDefaultValue(CapsuleCollider element) =>
            element.radius = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(CapsuleCollider element) =>
            element.radius =  _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}