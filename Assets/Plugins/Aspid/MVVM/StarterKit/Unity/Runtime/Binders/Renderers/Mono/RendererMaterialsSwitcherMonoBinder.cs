using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Renderer), "m_Materials")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder - Materials Switcher")]
    [AddComponentContextMenu(typeof(Renderer),"Add Renderer Binder/Renderer Binder - Materials Switcher")]
    public sealed class RendererMaterialsSwitcherMonoBinder : SwitcherMonoBinder<Renderer, Material[]>
    {
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        protected override void SetValue(Material[] values) =>
            CachedComponent.SetMaterials(_converter, values);
    }
}