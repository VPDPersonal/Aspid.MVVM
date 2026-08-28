#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{BoxCollider, Vector3}"/> that sets the <see cref="BoxCollider.size"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-BoxCollider-Size-1.1.0.xml" path="doc//member[@name='BoxColliderSizeBinder']/*" />
    [Serializable]
    public class BoxColliderSizeBinder : TargetBinder<BoxCollider, Vector3>, IVector3Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => Target.size;
            set => Target.size = this.NonNegative(value, Target);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public BoxColliderSizeBinder(
            BoxCollider target,
            IConverter<Vector3, Vector3>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}