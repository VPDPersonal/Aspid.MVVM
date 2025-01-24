using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Mesh, UnityEngine.Mesh>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterMesh;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder - Mesh")]
    public class MeshColliderMeshMonoBinder : ComponentMonoBinder<MeshCollider>, IBinder<Mesh>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        public void SetValue(Mesh value) =>
            CachedComponent.sharedMesh = _converter?.Convert(value) ?? value;
    }
}