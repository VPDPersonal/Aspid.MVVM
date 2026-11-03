using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Slider.value"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to the slider range.
    /// </remarks>
    [AddBinderContextMenu(
        type: typeof(Slider),
        serializePropertyNames: "m_Value",
        SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value Enum")]
    public sealed class SliderValueEnumMonoBinder : EnumMonoBinder<Slider, float>
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;

        /// <inheritdoc/>
        protected override void SetValue(float value)
        {
            var slider = CachedComponent;
            var clamped = this.SafeClamp(value, slider.minValue, slider.maxValue);

            if (_notifyMode.ShouldNotify(IsBound)) slider.value = clamped;
            else slider.SetValueWithoutNotify(clamped);
        }
    }
}
