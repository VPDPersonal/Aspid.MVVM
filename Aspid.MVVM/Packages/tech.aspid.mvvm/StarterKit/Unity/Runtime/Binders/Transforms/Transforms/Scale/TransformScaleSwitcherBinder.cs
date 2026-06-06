#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3Binder{Transform}"/> that switches the <see cref="Transform.localScale"/> between two
    /// <see cref="Vector3"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Transform-Scale-1.1.0.xml" path="doc//member[@name='TransformScaleSwitcherBinder']/*" />
    [Serializable]
    public sealed class TransformScaleSwitcherBinder : SwitcherVector3Binder<Transform>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TransformScaleSwitcherBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Transform"/> to bind.</param>
        /// <param name="trueValue">The scale applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The scale applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the selected <see cref="Vector3"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode.</param>
        public TransformScaleSwitcherBinder(
            Transform target, 
            Vector3 trueValue,
            Vector3 falseValue, 
            IConverter<Vector3, Vector3>? converter,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <summary>
        /// Called when applying the selected value to the <see cref="Transform.localScale"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            Target.localScale = value;
    }
}