#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Mesh?, UnityEngine.Mesh?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMesh;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class MeshColliderMeshBinder : TargetBinder<MeshCollider>, IBinder<Mesh>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public MeshColliderMeshBinder(MeshCollider target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public MeshColliderMeshBinder(MeshCollider target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _converter = converter;
        }

        public void SetValue(Mesh? value) =>
            Target.sharedMesh = _converter?.Convert(value) ?? value;
    }
}