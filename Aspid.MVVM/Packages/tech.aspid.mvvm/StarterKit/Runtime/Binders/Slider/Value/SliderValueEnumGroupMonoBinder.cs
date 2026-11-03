using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Slider.value"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to the slider range.
    /// </remarks>
    [AddBinderContextMenu(
        type: typeof(Slider),
        serializePropertyNames: "m_Value",
        SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value EnumGroup")]
    public sealed class SliderValueEnumGroupMonoBinder : EnumGroupMonoBinder<Slider, float>
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;

        /// <inheritdoc/>
        protected override void SetValue(Slider element, float value)
        {
            var clamped = this.SafeClamp(value, element.minValue, element.maxValue);

            if (_notifyMode.ShouldNotify(IsBound)) element.value = clamped;
            else element.SetValueWithoutNotify(clamped);
        }
    }
}
