#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TTarget,T}">SwitcherBinder&lt;MeshCollider, Mesh&gt;</see> that switches the <see cref="MeshCollider.sharedMesh"/>
    /// property between two <see cref="Mesh"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-MeshCollider-Mesh-1.1.0.xml" path="doc//member[@name='MeshColliderMeshSwitcherBinder']/*" />
    [Serializable]
    public sealed class MeshColliderMeshSwitcherBinder : SwitcherBinder<MeshCollider, Mesh>
    {
        /// <inheritdoc/>
        public MeshColliderMeshSwitcherBinder(
            MeshCollider target,
            Mesh trueValue,
            Mesh falseValue,
            IConverter<Mesh?, Mesh?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(Mesh value) =>
            Target.sharedMesh = value;
    }
}