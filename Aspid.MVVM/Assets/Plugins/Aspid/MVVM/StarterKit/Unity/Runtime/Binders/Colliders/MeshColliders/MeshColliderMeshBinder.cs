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
    public class MeshColliderMeshBinder : TargetBinder<MeshCollider, Mesh, Converter>
    {
        protected sealed override Mesh? Property
        {
            get => Target.sharedMesh;
            set => Target.sharedMesh = value;
        }
        
        public MeshColliderMeshBinder(MeshCollider target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public MeshColliderMeshBinder(MeshCollider target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}