#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{CapsuleCollider}"/> that sets the <see cref="CapsuleCollider.radius"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-CapsuleCollider-Radius-1.1.0.xml" path="doc//member[@name='CapsuleColliderRadiusBinder']/*" />
    [Serializable]
    public class CapsuleColliderRadiusBinder : TargetFloatBinder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.radius;
            set => Target.radius = value;
        }

        /// <inheritdoc/>
        public CapsuleColliderRadiusBinder(
            CapsuleCollider target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}