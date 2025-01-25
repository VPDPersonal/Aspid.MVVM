#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Mesh?, UnityEngine.Mesh?>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterMesh;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class MeshColliderMeshBinder : TargetBinder<MeshCollider>, IBinder<Mesh>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public MeshColliderMeshBinder(MeshCollider target, Func<Mesh?, Mesh?> converter)
            : this(target, converter.ToConvert()) { }
        
        public MeshColliderMeshBinder(MeshCollider target, Converter? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(Mesh? value) =>
            Target.sharedMesh = _converter?.Convert(value) ?? value;
    }
}