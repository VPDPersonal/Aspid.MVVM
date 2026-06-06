#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.PhysicsMaterial?, UnityEngine.PhysicsMaterial?>;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverterPhysicsMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{Collider, PhysicsMaterial, IConverter{PhysicsMaterial, PhysicsMaterial}}"/> that switches the <see cref="Collider.material"/>
    /// property between two <see cref="PhysicsMaterial"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Collider-Material-1.1.0.xml" path="doc//member[@name='ColliderMaterialSwitcherBinder']/*" />
    [Serializable]
    public sealed class ColliderMaterialSwitcherBinder : SwitcherBinder<Collider, PhysicsMaterial, Converter>
    {
        /// <inheritdoc/>
        public ColliderMaterialSwitcherBinder(
            Collider target,
            PhysicsMaterial trueValue,
            PhysicsMaterial falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(PhysicsMaterial? value) =>
            Target.material = value;
    }
}