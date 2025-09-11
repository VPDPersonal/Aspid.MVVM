using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(RawImage), "m_Material")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder - Material Switcher")]
    [AddComponentContextMenu(typeof(LineRenderer),"Add RawImage Binder/RawImage Binder - Material Switcher")]
    public sealed class RawImageMaterialSwitcherMonoBinder : SwitcherMonoBinder<RawImage, Material>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Material value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;
    }
}