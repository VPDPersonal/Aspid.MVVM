using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/Collider Binder - Material")]
    public class ColliderMaterialMonoBinder : ComponentMonoBinder<Collider>, IBinder<PhysicsMaterial>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<PhysicsMaterial, PhysicsMaterial> _converter;
#else 
        private IConverterPhysicsMaterial _converter;
#endif
        
        public void SetValue(PhysicsMaterial value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;
    }
}