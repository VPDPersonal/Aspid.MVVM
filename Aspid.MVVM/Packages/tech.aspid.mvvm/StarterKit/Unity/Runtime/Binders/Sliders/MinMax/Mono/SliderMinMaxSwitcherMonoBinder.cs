using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinderWithConverter{T1, T2}"/> that switches <see cref="Slider.minValue"/> and <see cref="Slider.maxValue"/> between two ranges based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – MinMax Switcher")]
    [AddBinderContextMenu(typeof(Slider), "m_MinValue", "m_MaxValue", SubPath = "Switcher")]
    public sealed class SliderMinMaxSwitcherMonoBinder : SwitcherMonoBinderWithConverter<Slider, Vector2>
    {
        [Tooltip("Determines which endpoint(s) of the slider range are updated.")]
        [SerializeField] private SliderValueMode _valueMode = SliderValueMode.Range;

        /// <summary>
        /// Called when applying the selected range to the slider using the configured <see cref="SliderValueMode"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(Vector2 value) =>
            CachedComponent.SetMinMax(value, _valueMode);
    }
}