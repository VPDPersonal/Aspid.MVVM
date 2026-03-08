using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="Slider.value"/> property on a group of <see cref="Slider"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value EnumGroup")]
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value", SubPath = "EnumGroup")]
    public sealed class SliderValueEnumGroupMonoBinder : EnumGroupMonoBinder<Slider, float, Converter>
    {
        protected override void SetValue(Slider element, float value) =>
            element.value = value;
    }
}