using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="Slider.value"/>.
    /// </summary>
    /// <remarks>
    /// {VR}
    /// </remarks>
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value Switcher")]
    public sealed class SliderValueSwitcherMonoBinder : SwitcherMonoBinder<Slider, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value)
        {
            var slider = CachedComponent;
            slider.SetValueWithoutNotify(this.SafeClamp(value, slider.minValue, slider.maxValue));
        }
    }
}
