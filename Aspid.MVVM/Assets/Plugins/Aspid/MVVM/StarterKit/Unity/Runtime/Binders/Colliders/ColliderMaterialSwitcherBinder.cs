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
    /// Binder that switches the <see cref="Collider.material"/> property on a <see cref="Collider"/>
    /// between two values based on a bound boolean ViewModel value.
    /// </summary>
    [Serializable]
    public sealed class ColliderMaterialSwitcherBinder : SwitcherBinder<Collider, PhysicsMaterial, Converter>
    {
        public ColliderMaterialSwitcherBinder(
            Collider target,
            PhysicsMaterial trueValue,
            PhysicsMaterial falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        public ColliderMaterialSwitcherBinder(
            Collider target,
            PhysicsMaterial trueValue,
            PhysicsMaterial falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(PhysicsMaterial? value) =>
            Target.material = value;
    }
}