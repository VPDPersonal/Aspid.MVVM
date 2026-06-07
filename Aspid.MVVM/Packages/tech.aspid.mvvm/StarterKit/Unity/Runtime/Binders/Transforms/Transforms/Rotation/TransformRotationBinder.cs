#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Quaternion, UnityEngine.Quaternion>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterQuaternion;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Transform, Quaternion, Converter}"/> that sets the <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/> property depending on the configured <see cref="Space"/>.
    /// </summary>
    /// <remarks>
    /// Also implements <see cref="INumberBinder"/> and <see cref="IRotationBinder"/>, allowing numeric values and
    /// direct <see cref="Quaternion"/> values to be bound.
    /// </remarks>
    /// <include file="XmlExampleDoc-Transform-Rotation-1.1.0.xml" path="doc//member[@name='TransformRotationBinder']/*" />
    [Serializable]
    public class TransformRotationBinder : TargetBinder<Transform, Quaternion, Converter>,
        INumberBinder,
        IRotationBinder
    {
        [Tooltip("The coordinate space in which the rotation is applied.")]
        [SerializeField] private Space _space;
        
        protected sealed override Quaternion Property
        {
            get => Target.GetRotation(_space);
            set => Target.SetRotation(value, _space);
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="TransformRotationBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Transform"/> to bind.</param>
        /// <param name="space">The coordinate space in which the rotation is applied.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Quaternion"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public TransformRotationBinder(
            Transform target,
            Space space = Space.World, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)    
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _space = space;
        }

        /// <summary>
        /// Converts the value to a <see cref="float"/> and applies a uniform <see cref="Quaternion.Euler"/> rotation.
        /// </summary>
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to a <see cref="float"/> and applies a uniform <see cref="Quaternion.Euler"/> rotation.
        /// </summary>
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to a <see cref="float"/> and applies a uniform <see cref="Quaternion.Euler"/> rotation.
        /// </summary>
        public void SetValue(double value) =>
            SetValue((float)value);

        /// <summary>
        /// Applies a uniform <see cref="Quaternion.Euler"/> rotation using the given angle on all three axes.
        /// </summary>
        public void SetValue(float value) =>
            base.SetValue(Quaternion.Euler(new Vector3(value, value, value)));
    }
}