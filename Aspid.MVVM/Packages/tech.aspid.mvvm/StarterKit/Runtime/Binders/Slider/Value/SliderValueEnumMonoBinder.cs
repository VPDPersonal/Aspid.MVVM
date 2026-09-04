using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Slider.value"/>.
    /// </summary>
    /// <remarks>
    /// {VR}
    /// </remarks>
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value Enum")]
    public sealed class SliderValueEnumMonoBinder : EnumMonoBinder<Slider, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value)
        {
            var slider = CachedComponent;
            slider.SetValueWithoutNotify(this.SafeClamp(value, slider.minValue, slider.maxValue));
        }
    }
}
