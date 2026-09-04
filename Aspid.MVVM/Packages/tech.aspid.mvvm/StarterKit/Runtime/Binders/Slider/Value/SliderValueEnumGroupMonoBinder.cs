using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Slider.value"/> on each element.
    /// </summary>
    /// <remarks>
    /// {VR}
    /// </remarks>
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value EnumGroup")]
    public sealed class SliderValueEnumGroupMonoBinder : EnumGroupMonoBinder<Slider, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(Slider element, float value) =>
            element.SetValueWithoutNotify(this.SafeClamp(value, element.minValue, element.maxValue));
    }
}
