using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Collider/SphereCollider Binder - Radius EnumGroup")]
    public sealed class SphereColliderRadiusEnumGroupMonoBinder : EnumGroupMonoBinder<SphereCollider>
    {
        [Header("Parameters")]
        [SerializeField] [Min(0)] private float _defaultValue;
        [SerializeField] [Min(0)] private float _selectedValue;
        
        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _defaultValueConverter;
#else
        private IConverterVector3 _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _selectedValueConverter;
#else
        private IConverterVector3 _selectedValueConverter;
#endif
        
        protected override void SetDefaultValue(SphereCollider element) =>
            element.radius = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(SphereCollider element) =>
            element.radius = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}