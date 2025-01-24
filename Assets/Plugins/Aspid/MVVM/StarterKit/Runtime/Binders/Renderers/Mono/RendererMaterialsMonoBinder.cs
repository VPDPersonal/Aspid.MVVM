using UnityEngine;
using System.Collections.Generic;
using Aspid.MVVM.Mono.Generation;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder - Materials")]
    public partial class RendererMaterialsMonoBinder : ComponentMonoBinder<Renderer>, IBinder<Material>, IBinder<Material[]>, IBinder<IReadOnlyCollection<Material>>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [BinderLog]
        public void SetValue(Material value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;

        [BinderLog]
        public void SetValue(Material[] values) =>
            CachedComponent.SetMaterials(_converter, values);
        
        [BinderLog]
        public void SetValue(IReadOnlyCollection<Material> values) =>
            CachedComponent.SetMaterials(_converter, values);
    }
}