#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
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
    public class ColliderMaterialBinder : TargetBinder<Collider>, IBinder<PhysicsMaterial>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown] 
        [SerializeReference] private Converter? _converter;

        public ColliderMaterialBinder(Collider target, Func<PhysicsMaterial?, PhysicsMaterial?> converter)
            : this(target, converter.ToConvert()) { }
        
        public ColliderMaterialBinder(Collider target, Converter? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(PhysicsMaterial? value) =>
            Target.material = _converter?.Convert(value) ?? value;
    }
}