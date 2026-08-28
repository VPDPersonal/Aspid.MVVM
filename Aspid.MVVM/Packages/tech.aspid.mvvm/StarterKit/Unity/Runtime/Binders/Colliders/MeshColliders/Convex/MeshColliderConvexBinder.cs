#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{MeshCollider, bool}"/> that sets the <see cref="MeshCollider.convex"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-MeshCollider-Convex-1.1.0.xml" path="doc//member[@name='MeshColliderConvexBinder']/*" />
    [Serializable]
    public class MeshColliderConvexBinder : TargetBinder<MeshCollider, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.convex;
            set => Target.convex = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public MeshColliderConvexBinder(MeshCollider target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            Mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}