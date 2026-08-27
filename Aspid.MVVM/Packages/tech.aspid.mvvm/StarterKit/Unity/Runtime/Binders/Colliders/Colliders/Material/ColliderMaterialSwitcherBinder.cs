#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinderWithConverter{T1, T2}">SwitcherBinderWithConverter&lt;Collider, PhysicsMaterial&gt;</see> that switches the <see cref="Collider.material"/>
    /// property between two <see cref="PhysicsMaterial"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Collider-Material-1.1.0.xml" path="doc//member[@name='ColliderMaterialSwitcherBinder']/*" />
    [Serializable]
    public sealed class ColliderMaterialSwitcherBinder : SwitcherBinderWithConverter<Collider, PhysicsMaterial>
    {
        /// <inheritdoc/>
        public ColliderMaterialSwitcherBinder(
            Collider target,
            PhysicsMaterial trueValue,
            PhysicsMaterial falseValue,
            IConverter<PhysicsMaterial?, PhysicsMaterial?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(PhysicsMaterial? value) =>
            Target.material = value;
    }
}