using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Graphic), "m_Color")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder - Color Switcher")]
    [AddComponentContextMenu(typeof(Graphic),"Add Graphic Binder/Graphic Binder - Color Switcher")]
    public sealed class GraphicColorSwitcherMonoBinder : SwitcherMonoBinder<Graphic, Color>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Color value) =>
            CachedComponent.color = _converter?.Convert(value) ?? value;
    }

}