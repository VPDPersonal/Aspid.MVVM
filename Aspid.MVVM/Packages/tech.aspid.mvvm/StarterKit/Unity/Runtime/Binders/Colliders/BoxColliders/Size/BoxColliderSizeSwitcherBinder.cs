#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TTarget,T}">SwitcherBinder&lt;BoxCollider, Vector3&gt;</see> that switches the <see cref="BoxCollider.size"/>
    /// property between two <see cref="Vector3"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-BoxCollider-Size-1.1.0.xml" path="doc//member[@name='BoxColliderSizeSwitcherBinder']/*" />
    [Serializable]
    public sealed class BoxColliderSizeSwitcherBinder : SwitcherBinder<BoxCollider, Vector3>
    {
        /// <inheritdoc/>
        public BoxColliderSizeSwitcherBinder(
            BoxCollider target,
            Vector3 trueValue,
            Vector3 falseValue,
            IConverter<Vector3, Vector3>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            Target.size = this.NonNegative(value, Target);
    }
}