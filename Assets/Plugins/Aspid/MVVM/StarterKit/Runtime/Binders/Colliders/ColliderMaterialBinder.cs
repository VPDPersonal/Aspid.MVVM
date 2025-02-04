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
    public class ColliderMaterialBinder : TargetBinder<Collider>, IBinder<PhysicsMaterial>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown] 
        [SerializeReference] private Converter? _converter;
        
        public ColliderMaterialBinder(Collider target, BindMode mode)
            : this(target, null, mode) { }
        
        public ColliderMaterialBinder(Collider target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _converter = converter;
        }

        public void SetValue(PhysicsMaterial? value) =>
            Target.material = _converter?.Convert(value) ?? value;
    }
}