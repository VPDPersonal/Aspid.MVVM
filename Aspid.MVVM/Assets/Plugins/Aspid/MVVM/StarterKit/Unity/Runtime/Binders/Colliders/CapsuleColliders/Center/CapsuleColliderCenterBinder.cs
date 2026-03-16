#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector3Binder{CapsuleCollider}"/> that sets the <see cref="CapsuleCollider.center"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-CapsuleCollider-Center-1.1.0.xml" path="doc//member[@name='CapsuleColliderCenterBinder']/*" />
    [Serializable]
    public class CapsuleColliderCenterBinder : TargetVector3Binder<CapsuleCollider>
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => Target.center;
            set => Target.center = value;
        }

        /// <inheritdoc/>
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