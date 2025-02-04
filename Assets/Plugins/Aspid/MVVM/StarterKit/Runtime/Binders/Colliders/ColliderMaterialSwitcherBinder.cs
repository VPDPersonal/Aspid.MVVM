#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.PhysicsMaterial?, UnityEngine.PhysicsMaterial?>;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterPhysicsMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ColliderMaterialSwitcherBinder : SwitcherBinder<Collider, PhysicsMaterial>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public ColliderMaterialSwitcherBinder(
            Collider target,
            PhysicsMaterial trueValue, 
            PhysicsMaterial falseValue, 
            BindMode mode)
            : this(target, trueValue, falseValue, null, mode) { }
        
        public ColliderMaterialSwitcherBinder(
            Collider target,
            PhysicsMaterial trueValue, 
            PhysicsMaterial falseValue, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(PhysicsMaterial? value) =>
            Target.material = _converter?.Convert(value) ?? value;
    }
}