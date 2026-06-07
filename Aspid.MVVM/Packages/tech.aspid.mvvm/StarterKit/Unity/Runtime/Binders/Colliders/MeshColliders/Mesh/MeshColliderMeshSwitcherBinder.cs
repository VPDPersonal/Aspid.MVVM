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
    /// <summary>
    /// <see cref="SwitcherBinder{MeshCollider, Mesh, IConverter{Mesh, Mesh}}"/> that switches the <see cref="MeshCollider.sharedMesh"/>
    /// property between two <see cref="Mesh"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-MeshCollider-Mesh-1.1.0.xml" path="doc//member[@name='MeshColliderMeshSwitcherBinder']/*" />
    [Serializable]
    public sealed class MeshColliderMeshSwitcherBinder : SwitcherBinder<MeshCollider, Mesh, Converter>
    {
        /// <inheritdoc/>
        public MeshColliderMeshSwitcherBinder(
            MeshCollider target,
            Mesh trueValue,
            Mesh falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(Mesh value) =>
            Target.sharedMesh = value;
    }
}