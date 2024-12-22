#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ColliderMaterialSwitcherBinder : SwitcherBinder<PhysicsMaterial>
    {
        [Header("Component")]
        [SerializeField] private Collider _collider;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif 
        private IConverter<PhysicsMaterial?, PhysicsMaterial?>? _converter;

        public ColliderMaterialSwitcherBinder(
            PhysicsMaterial trueValue, 
            PhysicsMaterial falseValue, 
            Collider collider,
            Func<PhysicsMaterial?, PhysicsMaterial?> converter)
            : this(trueValue, falseValue, collider, converter.ToConvert()) { }
        
        public ColliderMaterialSwitcherBinder(
            PhysicsMaterial trueValue, 
            PhysicsMaterial falseValue, 
            Collider collider,
            IConverter<PhysicsMaterial?, PhysicsMaterial?>? converter = null)
            : base(trueValue, falseValue)
        {
            _converter = converter;
            _collider = collider ?? throw new ArgumentNullException(nameof(collider));
        }

        protected override void SetValue(PhysicsMaterial? value) =>
            _collider.material = _converter?.Convert(value) ?? value;
    }
}