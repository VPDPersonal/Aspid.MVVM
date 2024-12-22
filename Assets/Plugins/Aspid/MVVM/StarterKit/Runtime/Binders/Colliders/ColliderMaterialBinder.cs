#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ColliderMaterialBinder : Binder, IBinder<PhysicsMaterial>
    {
        [Header("Component")]
        [SerializeField] private Collider _collider;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif 
        private IConverter<PhysicsMaterial?, PhysicsMaterial?>? _converter;

        public ColliderMaterialBinder(Collider collider, Func<PhysicsMaterial?, PhysicsMaterial?> converter)
            : this(collider, converter.ToConvert()) { }
        
        public ColliderMaterialBinder(Collider collider, IConverter<PhysicsMaterial?, PhysicsMaterial?>? converter = null)
        {
            _converter = converter;
            _collider = collider ?? throw new ArgumentNullException(nameof(collider));
        }

        public void SetValue(PhysicsMaterial? value) =>
            _collider.material = _converter?.Convert(value) ?? value;
    }
}