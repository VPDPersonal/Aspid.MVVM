using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Mesh, UnityEngine.Mesh>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterMesh;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(MeshCollider), "m_Mesh")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder - Mesh EnumGroup")]
    [AddComponentContextMenu(typeof(MeshCollider),"Add MeshCollider Binder/MeshCollider Binder - Mesh EnumGroup")]
    public sealed class MeshColliderMeshEnumGroupMonoBinder : EnumGroupMonoBinder<MeshCollider>
    {
        [Header("Parameters")]
        [SerializeField] private Mesh _defaultValue;
        [SerializeField] private Mesh _selectedValue;
        
        [Header("Converters")]
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