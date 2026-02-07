using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.PhysicsMaterial, UnityEngine.PhysicsMaterial>;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverterPhysicsMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material Enum")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material", SubPath = "Enum")]
    public sealed class ColliderMaterialEnumMonoBinder : EnumMonoBinder<Collider, PhysicsMaterial>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(PhysicsMaterial value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;
    }
}