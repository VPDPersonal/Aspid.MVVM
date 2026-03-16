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
    /// <see cref="TargetBinder{MeshCollider, Mesh, IConverter{Mesh, Mesh}}"/> that sets the <see cref="MeshCollider.sharedMesh"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-MeshCollider-Mesh-1.1.0.xml" path="doc//member[@name='MeshColliderMeshBinder']/*" />
    [Serializable]
    public class MeshColliderMeshBinder : TargetBinder<MeshCollider, Mesh, Converter>
    {
        /// <inheritdoc/>
        protected sealed override Mesh? Property
        {
            get => Target.sharedMesh;
            set => Target.sharedMesh = value;
        }

        /// <inheritdoc/>
        public MeshColliderMeshBinder(MeshCollider target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}