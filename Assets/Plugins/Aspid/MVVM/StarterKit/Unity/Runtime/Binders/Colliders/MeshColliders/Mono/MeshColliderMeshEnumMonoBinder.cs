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
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder - Mesh Enum")]
    [AddComponentContextMenu(typeof(MeshCollider),"Add MeshCollider Binder/MeshCollider Binder - Mesh Enum")]
    public sealed class MeshColliderMeshEnumMonoBinder : EnumMonoBinder<MeshCollider, Mesh>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Mesh value) =>
            CachedComponent.sharedMesh = _converter?.Convert(value) ?? value;
    }
}