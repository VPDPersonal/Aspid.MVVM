using UnityEngine;
using System.Collections.Generic;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials")]
    public partial class RendererMaterialsMonoBinder : ComponentMonoBinder<Renderer>, IBinder<Material>, IBinder<Material[]>, IBinder<IReadOnlyCollection<Material>>
    {
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