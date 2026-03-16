#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3Binder{CapsuleCollider}"/> that switches the <see cref="CapsuleCollider.center"/>
    /// property between two <see cref="Vector3"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-CapsuleCollider-Center-1.1.0.xml" path="doc//member[@name='CapsuleColliderCenterSwitcherBinder']/*" />
    [Serializable]
    public sealed class CapsuleColliderCenterSwitcherBinder : SwitcherVector3Binder<CapsuleCollider>
    {
        /// <inheritdoc/>
        public CapsuleColliderCenterSwitcherBinder(
            CapsuleCollider target,
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