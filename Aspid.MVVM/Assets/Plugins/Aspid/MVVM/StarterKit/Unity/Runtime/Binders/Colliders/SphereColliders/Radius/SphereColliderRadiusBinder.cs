#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{SphereCollider}"/> that sets the <see cref="SphereCollider.radius"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-SphereCollider-Radius-1.1.0.xml" path="doc//member[@name='SphereColliderRadiusBinder']/*" />
    [Serializable]
    public class SphereColliderRadiusBinder : TargetFloatBinder<SphereCollider>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.radius;
            set => Target.radius = value;
        }

        /// <inheritdoc/>
        public SphereColliderRadiusBinder(
            SphereCollider target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}