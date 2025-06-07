using UnityEngine;
using Aspid.MVVM.Unity;
using System.Collections.Generic;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Renderer), "m_Materials")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder - Materials")]
    [AddComponentContextMenu(typeof(Renderer),"Add Renderer Binder/Renderer Binder - Materials")]
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