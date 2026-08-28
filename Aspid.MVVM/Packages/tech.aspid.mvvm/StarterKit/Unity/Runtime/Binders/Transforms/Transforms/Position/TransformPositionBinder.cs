#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Transform, Vector3}"/> that sets the <see cref="Transform.position"/> or
    /// <see cref="Transform.localPosition"/> property depending on the configured <see cref="Space"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-Transform-Position-1.1.0.xml" path="doc//member[@name='TransformPositionBinder']/*" />
    [Serializable]
    public class TransformPositionBinder : TargetBinder<Transform, Vector3>, IVector3Binder
    {
        [Tooltip("The coordinate space in which the position is applied.")]
        [SerializeField] private Space _space;

        protected sealed override Vector3 Property
        {
            get => Target.GetPosition(_space);
            set => Target.SetPosition(value, _space);
        }

        /// <param name="target">The <see cref="Transform"/> to bind.</param>
        /// <param name="space">The coordinate space in which the position is applied.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Vector3"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public TransformPositionBinder(
            Transform target,
            Space space = Space.World,
            IConverter<Vector3, Vector3>? converter = null, 
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _space = space;
        }
    }
}