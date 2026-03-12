#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{BoxCollider, Vector3, IConverter{Vector3, Vector3}}"/> that switches the <see cref="BoxCollider.center"/>
    /// property between two <see cref="Vector3"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-BoxCollider-Center-1.1.0.xml" path="doc//member[@name='BoxColliderCenterSwitcherBinder']/*" />
    [Serializable]
    public sealed class BoxColliderCenterSwitcherBinder : SwitcherVector3Binder<BoxCollider>
    {
        /// <inheritdoc/>
        public BoxColliderCenterSwitcherBinder(
            BoxCollider target,
            Vector3 trueValue,
            Vector3 falseValue,
            IConverter<Vector3, Vector3>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            Target.center = value;
    }
}