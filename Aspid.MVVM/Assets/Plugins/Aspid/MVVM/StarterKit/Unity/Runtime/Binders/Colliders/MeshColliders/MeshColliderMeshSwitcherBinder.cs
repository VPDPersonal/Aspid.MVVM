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
    public sealed class MeshColliderMeshSwitcherBinder : SwitcherBinder<MeshCollider, Mesh, Converter>
    {
        public MeshColliderMeshSwitcherBinder(
            MeshCollider target,
            Mesh trueValue, 
            Mesh falseValue, 
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public MeshColliderMeshSwitcherBinder(
            MeshCollider target,
            Mesh trueValue, 
            Mesh falseValue, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(Mesh value) =>
            Target.sharedMesh = value;
    }
}