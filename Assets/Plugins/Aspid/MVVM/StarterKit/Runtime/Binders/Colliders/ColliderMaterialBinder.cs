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
    public class ColliderMaterialBinder : TargetBinder<Collider>, IBinder<PhysicsMaterial>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif 
        private IConverter<PhysicsMaterial?, PhysicsMaterial?>? _converter;

        public ColliderMaterialBinder(Collider target, Func<PhysicsMaterial?, PhysicsMaterial?> converter)
            : this(target, converter.ToConvert()) { }
        
        public ColliderMaterialBinder(Collider target, IConverter<PhysicsMaterial?, PhysicsMaterial?>? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(PhysicsMaterial? value) =>
            Target.material = _converter?.Convert(value) ?? value;
    }
}