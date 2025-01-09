using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/Collider Binder - Material EnumGroup")]
    public sealed class ColliderMaterialEnumGroupMonoBinder : EnumGroupMonoBinder<Collider>
    {
        [Header("Parameters")]
        [SerializeField] private PhysicsMaterial _defaultValue;
        [SerializeField] private PhysicsMaterial _selectedValue;
        
        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<PhysicsMaterial, PhysicsMaterial> _defaultValueConverter;
#else 
        private IConverterPhysicsMaterial _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<PhysicsMaterial, PhysicsMaterial> _selectedValueConverter;
#else 
        private IConverterPhysicsMaterial _selectedValueConverter;
#endif

        protected override void SetDefaultValue(Collider element) =>
            element.material = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(Collider element) =>
            element.material = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}