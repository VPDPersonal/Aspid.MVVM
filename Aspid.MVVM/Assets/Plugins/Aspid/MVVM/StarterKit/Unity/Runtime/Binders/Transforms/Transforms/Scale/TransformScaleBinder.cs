#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector3Binder{Transform}"/> that sets the <see cref="Transform.localScale"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Transform-Scale-1.1.0.xml" path="doc//member[@name='TransformScaleBinder']/*" />
    [Serializable]
    public class TransformScaleBinder : TargetVector3Binder<Transform>
    {
        protected sealed override Vector3 Property
        {
            get => Target.localScale;
            set => Target.localScale = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TransformScaleBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Transform"/> to bind.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Vector3"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public TransformScaleBinder(Transform target, IConverter<Vector3, Vector3>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}