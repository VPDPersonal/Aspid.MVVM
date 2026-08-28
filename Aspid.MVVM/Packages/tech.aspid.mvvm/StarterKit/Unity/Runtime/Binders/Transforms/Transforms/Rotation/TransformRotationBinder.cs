#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Transform, Quaternion}"/> that sets the <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/> property depending on the configured <see cref="Space"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-Transform-Rotation-1.1.0.xml" path="doc//member[@name='TransformRotationBinder']/*" />
    [Serializable]
    public class TransformRotationBinder : TargetBinder<Transform, Quaternion>, IRotationBinder
    {
        [Tooltip("The coordinate space in which the rotation is applied.")]
        [SerializeField] private Space _space;
        
        protected sealed override Quaternion Property
        {
            get => Target.GetRotation(_space);
            set => Target.SetRotation(value, _space);
        }
        
        /// <param name="target">The <see cref="Transform"/> to bind.</param>
        /// <param name="space">The coordinate space in which the rotation is applied.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Quaternion"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — a rotation property raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public TransformRotationBinder(
            Transform target,
            Space space = Space.World,
            IConverter<Quaternion, Quaternion>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _space = space;
        }
    }
}