using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Collider/MeshCollider Binder - Mesh")]
    public class MeshColliderMeshMonoBinder : ComponentMonoBinder<MeshCollider>, IBinder<Mesh>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Mesh, Mesh> _converter;
#else
        private IConverterMesh _converter;
#endif

        public void SetValue(Mesh value) =>
            CachedComponent.sharedMesh = _converter?.Convert(value) ?? value;
    }
}