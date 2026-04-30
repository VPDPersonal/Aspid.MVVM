#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatBinder{SphereCollider}"/> that switches the <see cref="SphereCollider.radius"/>
    /// property between two <see langword="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-SphereCollider-Radius-1.1.0.xml" path="doc//member[@name='SphereColliderRadiusSwitcherBinder']/*" />
    [Serializable]
    public sealed class SphereColliderRadiusSwitcherBinder : SwitcherFloatBinder<SphereCollider>
    {
        /// <inheritdoc/>
        public SphereColliderRadiusSwitcherBinder(
            SphereCollider target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.radius = value;
    }
}