#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3Binder{Transform}"/> that switches the <see cref="Transform.eulerAngles"/> or
    /// <see cref="Transform.localEulerAngles"/> between two <see cref="Vector3"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Transform-EulerAngles-1.1.0.xml" path="doc//member[@name='TransformEulerAnglesSwitcherBinder']/*" />
    [Serializable]
    public sealed class TransformEulerAnglesSwitcherBinder : SwitcherVector3Binder<Transform>
    {
        [Tooltip("The coordinate space in which the euler angles are applied.")]
        [SerializeField] private Space _space;
        
        /// <summary>
        /// Initializes a new instance of <see cref="TransformEulerAnglesSwitcherBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Transform"/> to bind.</param>
        /// <param name="trueValue">The euler angles applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The euler angles applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="space">The coordinate space in which the euler angles are applied.</param>
        /// <param name="converter">The converter used to transform the selected <see cref="Vector3"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode.</param>
        public TransformEulerAnglesSwitcherBinder(
            Transform target, 
            Vector3 trueValue, 
            Vector3 falseValue, 
            Space space = Space.World,
            IConverter<Vector3, Vector3>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _space = space;
        }

        /// <summary>
        /// Called when applying the selected value to the euler angles of the <see cref="Transform"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            Target.SetEulerAngles(value, _space);
    }
}