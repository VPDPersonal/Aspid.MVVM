using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{Slider}"/> that switches <see cref="Slider.value"/>
    /// between two float values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value Switcher")]
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value", SubPath = "Switcher")]
    public sealed class SliderValueSwitcherMonoBinder : SwitcherFloatMonoBinder<Slider>
    {
        /// <summary>
        /// Called when applying the selected value to <see cref="Slider.value"/>.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.value = value;
    }
}