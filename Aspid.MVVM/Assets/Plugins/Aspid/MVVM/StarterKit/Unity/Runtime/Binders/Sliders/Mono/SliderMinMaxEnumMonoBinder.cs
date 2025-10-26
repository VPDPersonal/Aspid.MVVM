using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Slider), "m_MinValue")]
    [AddPropertyContextMenu(typeof(Slider), "m_MaxValue")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder - MinMax Enum")]
    [AddComponentContextMenu(typeof(Slider),"Add Slider Binder/Slider Binder - MinMax Enum")]
    public sealed class SliderMinMaxEnumMonoBinder : EnumMonoBinder<Slider, Vector2>
    {
        [SerializeField] private SliderValueMode _valueMode = SliderValueMode.Range;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.SetMinMax(value, _valueMode);
        }
    }
}