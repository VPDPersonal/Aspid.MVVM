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
    [AddPropertyContextMenu(typeof(Collider), "m_Material")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder - Material")]
    [AddComponentContextMenu(typeof(Collider),"Add Binder/Collider Binder - Material")]
    public partial class ColliderMaterialMonoBinder : ComponentMonoBinder<Collider>, IBinder<PhysicsMaterial>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [BinderLog]
        public void SetValue(PhysicsMaterial value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;
    }
}