using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Slider.minValue"/> and
    /// <see cref="Slider.maxValue"/>.
    /// </summary>
    /// <remarks>
    /// {MMR}
    /// </remarks>
    [AddBinderContextMenu(typeof(Slider), "m_MinValue", "m_MaxValue", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – MinMax Enum")]
    public sealed class SliderMinMaxEnumMonoBinder : EnumMonoBinder<Slider, Vector2>
    {
        [Tooltip("Which endpoints the value writes.")]
        [SerializeField] private SliderRangeMode _rangeMode = SliderRangeMode.Range;

        /// <inheritdoc/>
        protected override void SetValue(Vector2 value) =>
            CachedComponent.SetMinMax(value, _rangeMode);
    }
}
