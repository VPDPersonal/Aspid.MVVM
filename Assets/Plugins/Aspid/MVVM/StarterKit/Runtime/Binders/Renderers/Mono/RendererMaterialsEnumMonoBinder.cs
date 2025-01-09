using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Renderer/Renderer Binder - Materials Enum")]
    public sealed class RendererMaterialsEnumMonoBinder : EnumComponentMonoBinder<Renderer, Material[]>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Material, Material> _converter;
#else
        private IConverterMaterial _converter;
#endif
        
        protected override void SetValue(Material[] values) =>
            CachedComponent.SetMaterials(_converter, values);
    }
}