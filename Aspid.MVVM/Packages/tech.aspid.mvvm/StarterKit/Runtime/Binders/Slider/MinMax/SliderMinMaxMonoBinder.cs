using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Slider.minValue"/> and
    /// <see cref="Slider.maxValue"/> as <c>(min, max)</c>.
    /// </summary>
    /// <remarks>
    /// See <see cref="SliderExtensions.SetMinMax"/> for how the range is validated.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Slider), "m_MinValue", "m_MaxValue")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – MinMax")]
    public class SliderMinMaxMonoBinder : ComponentMonoBinder<Slider, Vector2>, IVector2Binder
    {
        [Tooltip("Which endpoints the value writes.")]
        [SerializeField] private SliderRangeMode _rangeMode = SliderRangeMode.Range;

        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => new(CachedComponent.minValue, CachedComponent.maxValue);
            set => CachedComponent.SetMinMax(value, _rangeMode);
        }
    }
}
