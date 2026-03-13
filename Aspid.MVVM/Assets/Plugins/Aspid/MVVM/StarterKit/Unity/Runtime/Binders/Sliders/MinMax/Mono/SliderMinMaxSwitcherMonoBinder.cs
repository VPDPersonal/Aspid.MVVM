using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{Slider, Vector2, Converter}"/> that switches <see cref="Slider.minValue"/> and <see cref="Slider.maxValue"/> between two ranges based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – MinMax Switcher")]
    [AddBinderContextMenu(typeof(Slider), "m_MinValue", "m_MaxValue", SubPath = "Switcher")]
    public sealed class SliderMinMaxSwitcherMonoBinder : SwitcherMonoBinder<Slider, Vector2, Converter>
    {
        [Tooltip("Determines which endpoint(s) of the slider range are updated.")]
        [SerializeField] private SliderValueMode _valueMode = SliderValueMode.Range;

        /// <summary>
        /// Called when applying the selected range to the slider using the configured <see cref="SliderValueMode"/>.
        /// </summary>
        protected override void SetValue(Vector2 value) =>
            CachedComponent.SetMinMax(value, _valueMode);
    }
}