#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector3Binder{SphereCollider}"/> that sets the <see cref="SphereCollider.center"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-SphereCollider-Center-1.1.0.xml" path="doc//member[@name='SphereColliderCenterBinder']/*" />
    [Serializable]
    public class SphereColliderCenterBinder : TargetVector3Binder<SphereCollider>
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => Target.center;
            set => Target.center = value;
        }

        /// <inheritdoc/>
        public SphereColliderCenterBinder(
            SphereCollider target,
            IConverter<Vector3, Vector3>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}