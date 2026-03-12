#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{MeshCollider}"/> that sets the <see cref="MeshCollider.convex"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-MeshCollider-Convex-1.1.0.xml" path="doc//member[@name='MeshColliderConvexBinder']/*" />
    [Serializable]
    public class MeshColliderConvexBinder : TargetBoolBinder<MeshCollider>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.convex;
            set => Target.convex = value;
        }

        /// <inheritdoc/>
        public MeshColliderConvexBinder(MeshCollider target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            Mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}