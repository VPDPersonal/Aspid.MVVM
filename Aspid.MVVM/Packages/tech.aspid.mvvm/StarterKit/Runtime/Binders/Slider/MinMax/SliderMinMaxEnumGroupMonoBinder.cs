using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Slider.minValue"/> and
    /// <see cref="Slider.maxValue"/> on each element.
    /// </summary>
    /// <remarks>
    /// {MMR}
    /// </remarks>
    [AddBinderContextMenu(typeof(Slider), "m_MinValue", "m_MaxValue", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – MinMax EnumGroup")]
    public sealed class SliderMinMaxEnumGroupMonoBinder : EnumGroupMonoBinder<Slider, Vector2>
    {
        [Tooltip("Which endpoints the value writes.")]
        [SerializeField] private SliderRangeMode _rangeMode = SliderRangeMode.Range;

        /// <inheritdoc/>
        protected override void SetValue(Slider element, Vector2 value) =>
            element.SetMinMax(value, _rangeMode);
    }
}
