#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{CapsuleCollider, Vector3}"/> that sets the <see cref="CapsuleCollider.center"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-CapsuleCollider-Center-1.1.0.xml" path="doc//member[@name='CapsuleColliderCenterBinder']/*" />
    [Serializable]
    public class CapsuleColliderCenterBinder : TargetBinder<CapsuleCollider, Vector3>, IVector3Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => Target.center;
            set => Target.center = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public CapsuleColliderCenterBinder(
            CapsuleCollider target,
            IConverter<Vector3, Vector3>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}