using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Mesh, UnityEngine.Mesh>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMesh;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh Switcher")]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh", SubPath = "Switcher")]
    public sealed class MeshColliderMeshSwitcherMonoBinder : SwitcherMonoBinder<MeshCollider, Mesh>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Mesh value) =>
            CachedComponent.sharedMesh = _converter?.Convert(value) ?? value;
    }
}