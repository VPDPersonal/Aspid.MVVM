#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class MeshColliderMeshBinder : TargetBinder<MeshCollider>, IBinder<Mesh>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Mesh?, Mesh?>? _converter;

        public MeshColliderMeshBinder(MeshCollider target, Func<Mesh?, Mesh?> converter)
            : this(target, converter.ToConvert()) { }
        
        public MeshColliderMeshBinder(MeshCollider target, IConverter<Mesh?, Mesh?>? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(Mesh? value) =>
            Target.sharedMesh = _converter?.Convert(value) ?? value;
    }
}