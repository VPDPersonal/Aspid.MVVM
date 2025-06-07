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
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder - Mesh Switcher")]
    [AddComponentContextMenu(typeof(MeshCollider),"Add MeshCollider Binder/MeshCollider Binder - Mesh Switcher")]
    public sealed class MeshColliderMeshSwitcherMonoBinder : SwitcherMonoBinder<MeshCollider, Mesh>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Mesh value) =>
            CachedComponent.sharedMesh = _converter?.Convert(value) ?? value;
    }
}