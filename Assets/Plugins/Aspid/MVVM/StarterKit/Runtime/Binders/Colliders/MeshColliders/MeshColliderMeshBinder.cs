#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class MeshColliderMeshBinder : Binder, IBinder<Mesh>
    {
        [Header("Component")]
        [SerializeField] private MeshCollider _collider;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Mesh?, Mesh?>? _converter;

        public MeshColliderMeshBinder(MeshCollider collider, Func<Mesh?, Mesh?> converter)
            : this(collider, converter.ToConvert()) { }
        
        public MeshColliderMeshBinder(MeshCollider collider, IConverter<Mesh?, Mesh?>? converter = null)
        {
            _converter = converter;
            _collider = collider ?? throw new ArgumentNullException(nameof(collider));
        }

        public void SetValue(Mesh? value) =>
            _collider.sharedMesh = _converter?.Convert(value) ?? value;
    }
}