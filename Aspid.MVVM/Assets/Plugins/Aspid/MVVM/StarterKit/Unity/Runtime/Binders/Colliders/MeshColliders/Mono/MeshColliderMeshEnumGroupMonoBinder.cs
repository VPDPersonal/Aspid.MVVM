using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Mesh, UnityEngine.Mesh>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMesh;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh EnumGroup")]
    public sealed class MeshColliderMeshEnumGroupMonoBinder : EnumGroupMonoBinder<MeshCollider>
    {
        [SerializeField] private Mesh _defaultValue;
        [SerializeField] private Mesh _selectedValue;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
        protected override void SetDefaultValue(MeshCollider element) =>
            element.sharedMesh = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(MeshCollider element) =>
            element.sharedMesh = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}