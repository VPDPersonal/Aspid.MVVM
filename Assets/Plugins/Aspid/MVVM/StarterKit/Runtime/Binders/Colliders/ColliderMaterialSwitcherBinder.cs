#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ColliderMaterialSwitcherBinder : SwitcherBinder<Collider, PhysicsMaterial>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif 
        private IConverter<PhysicsMaterial?, PhysicsMaterial?>? _converter;

        public ColliderMaterialSwitcherBinder(
            Collider target,
            PhysicsMaterial trueValue, 
            PhysicsMaterial falseValue, 
            Func<PhysicsMaterial?, PhysicsMaterial?> converter)
            : this(target, trueValue, falseValue, converter.ToConvert()) { }
        
        public ColliderMaterialSwitcherBinder(
            Collider target,
            PhysicsMaterial trueValue, 
            PhysicsMaterial falseValue, 
            IConverter<PhysicsMaterial?, PhysicsMaterial?>? converter = null)
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(PhysicsMaterial? value) =>
            Target.material = _converter?.Convert(value) ?? value;
    }
}