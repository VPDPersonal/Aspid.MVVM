using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.PhysicsMaterial, UnityEngine.PhysicsMaterial>;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterPhysicsMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Collider), "m_Material")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - Material Enum")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - Material Enum")]
    public sealed class ColliderMaterialEnumMonoBinder : EnumComponentMonoBinder<Collider, PhysicsMaterial>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(PhysicsMaterial value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;
    }
}