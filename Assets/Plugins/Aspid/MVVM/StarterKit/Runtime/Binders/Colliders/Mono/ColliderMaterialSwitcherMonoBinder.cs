using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Collider/Collider Binder - Material Switcher")]
    public sealed class ColliderMaterialSwitcherMonoBinder : SwitcherMonoBinder<Collider, PhysicsMaterial>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<PhysicsMaterial, PhysicsMaterial> _converter;
#else 
        private IConverterPhysicsMaterial _converter;
#endif
        
        protected override void SetValue(PhysicsMaterial value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;
    }
}