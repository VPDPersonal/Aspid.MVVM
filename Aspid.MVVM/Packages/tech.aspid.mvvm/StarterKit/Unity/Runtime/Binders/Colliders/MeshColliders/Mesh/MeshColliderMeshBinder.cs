#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinderWithConverter{T1, T2}">TargetBinderWithConverter&lt;MeshCollider, Mesh&gt;</see> that sets the <see cref="MeshCollider.sharedMesh"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-MeshCollider-Mesh-1.1.0.xml" path="doc//member[@name='MeshColliderMeshBinder']/*" />
    [Serializable]
    public class MeshColliderMeshBinder : TargetBinderWithConverter<MeshCollider, Mesh>
    {
        /// <inheritdoc/>
        protected sealed override Mesh? Property
        {
            get => Target.sharedMesh;
            set => Target.sharedMesh = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public MeshColliderMeshBinder(MeshCollider target, IConverter<Mesh?, Mesh?>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}