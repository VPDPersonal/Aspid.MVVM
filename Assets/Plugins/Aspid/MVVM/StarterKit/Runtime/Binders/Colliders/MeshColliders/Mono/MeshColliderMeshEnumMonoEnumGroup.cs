using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/Mesh/MeshCollider Binder - Mesh EnumGroup")]
    public sealed class MeshColliderMeshEnumMonoEnumGroup : EnumGroupMonoBinder<MeshCollider>
    {
        [Header("Parameters")]
        [SerializeField] private Mesh _defaultValue;
        [SerializeField] private Mesh _selectedValue;
        
        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Mesh, Mesh> _defaultValueConverter;
#else
        private IConverterMesh _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Mesh, Mesh> _selectedValueConverter;
#else
        private IConverterMesh _selectedValueConverter;
#endif
        
        protected override void SetDefaultValue(MeshCollider element) =>
            element.sharedMesh = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(MeshCollider element) =>
            element.sharedMesh = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}