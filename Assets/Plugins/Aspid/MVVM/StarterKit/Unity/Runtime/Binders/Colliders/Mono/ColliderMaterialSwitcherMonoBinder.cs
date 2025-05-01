using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.PhysicsMaterial, UnityEngine.PhysicsMaterial>;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterPhysicsMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - Material Switcher")]
    public sealed class ColliderMaterialSwitcherMonoBinder : SwitcherMonoBinder<Collider, PhysicsMaterial>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(PhysicsMaterial value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;
    }
}