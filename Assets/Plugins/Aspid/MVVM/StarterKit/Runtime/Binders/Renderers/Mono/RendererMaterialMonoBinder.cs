using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Renderer/Renderer Binder - Material")]
    public partial class RendererMaterialMonoBinder : ComponentMonoBinder<Renderer>, IBinder<Material>, IBinder<Material[]>
    {
        [Header("Converter")]
#if ASPID_MVVM_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
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

        [BinderLog]
        public void SetValue(Material[] value)
        {
            if (_converter is not null)
            {
                for (var i = 0; i < value.Length; i++)
                    value[i] = _converter.Convert(value[i]);
            }
            
            CachedComponent.materials = value;
        }
    }
}