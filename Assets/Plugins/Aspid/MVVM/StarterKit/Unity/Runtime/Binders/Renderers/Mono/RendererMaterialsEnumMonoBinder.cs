using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Renderer), "m_Materials")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder - Materials Enum")]
    [AddComponentContextMenu(typeof(Renderer),"Add Renderer Binder/Renderer Binder - Materials Enum")]
    public sealed class RendererMaterialsEnumMonoBinder : EnumComponentMonoBinder<Renderer, Material[]>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Material[] values) =>
            CachedComponent.SetMaterials(_converter, values);
    }
}