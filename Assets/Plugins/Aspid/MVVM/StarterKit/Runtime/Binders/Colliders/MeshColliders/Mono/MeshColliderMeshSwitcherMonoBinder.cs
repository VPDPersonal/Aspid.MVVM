using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Collider/MeshCollider Binder - Mesh Switcher")]
    public sealed class MeshColliderMeshSwitcherMonoBinder : SwitcherMonoBinder<MeshCollider, Mesh>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Mesh, Mesh> _converter;
#else
        private IConverterMesh _converter;
#endif
        
        protected override void SetValue(Mesh value) =>
            CachedComponent.sharedMesh = _converter?.Convert(value) ?? value;
    }
}