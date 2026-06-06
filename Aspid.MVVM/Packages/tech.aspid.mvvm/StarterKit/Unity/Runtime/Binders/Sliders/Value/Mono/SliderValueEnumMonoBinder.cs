using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{Slider}"/> that sets <see cref="Slider.value"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value Enum")]
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value", SubPath = "Enum")]
    public sealed class SliderValueEnumMonoBinder : EnumFloatMonoBinder<Slider>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="Slider.value"/> to the resolved float.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.value = value;
    }
}