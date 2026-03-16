using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{Slider}"/> that sets <see cref="Slider.value"/>
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value EnumGroup")]
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value", SubPath = "EnumGroup")]
    public sealed class SliderValueEnumGroupMonoBinder : EnumGroupFloatMonoBinder<Slider>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="Slider.value"/> of the element to the resolved float.
        /// </summary>
        protected override void SetValue(Slider element, float value) =>
            element.value = value;
    }
}