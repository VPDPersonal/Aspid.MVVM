#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector3Binder{BoxCollider}"/> that sets the <see cref="BoxCollider.center"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-BoxCollider-Center-1.1.0.xml" path="doc//member[@name='BoxColliderCenterBinder']/*" />
    [Serializable]
    public class BoxColliderCenterBinder : TargetVector3Binder<BoxCollider>
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => Target.center;
            set => Target.center = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public BoxColliderCenterBinder(BoxCollider target, IConverter<Vector3, Vector3>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}