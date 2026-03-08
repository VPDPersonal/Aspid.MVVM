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
    /// MonoBehaviour binder that switches the <see cref="Slider.value"/> property on a <see cref="Slider"/>
    /// between two values based on a bound boolean ViewModel property.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value Switcher")]
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value", SubPath = "Switcher")]
    public sealed class SliderValueSwitcherMonoBinder : SwitcherMonoBinder<Slider, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.value = value;
    }
}