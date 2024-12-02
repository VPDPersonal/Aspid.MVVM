using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Material")]
    public partial class RawImageMaterialMonoBinder : ComponentMonoBinder<RawImage>, IBinder<Material>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<Material, Material> _converter;
#else
        [SerializeReference] private IConverterMaterialToMaterial _converter;
#endif
        
        [BinderLog]
        public void SetValue(Material value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;
    }
}