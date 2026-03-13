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
    /// <see cref="ComponentMonoBinder{Slider, Vector2, Converter}"/> that sets the minimum and maximum
    /// values of a <see cref="Slider"/> when the bound ViewModel value changes.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current
    /// slider range is immediately forwarded to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – MinMax")]
    [AddBinderContextMenu(typeof(Slider), "m_MinValue", "m_MaxValue")]
    public partial class SliderMinMaxMonoBinder : ComponentMonoBinder<Slider, Vector2, Converter>, INumberBinder
    {
        [Tooltip("Determines which endpoint(s) of the slider range are updated.")]
        [SerializeField] private SliderValueMode _valueMode = SliderValueMode.Range;

        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => new(CachedComponent.minValue, CachedComponent.maxValue);
            set => CachedComponent.SetMinMax(value, _valueMode);
        }

        /// <summary>
        /// Sets both <see cref="Slider.minValue"/> and <see cref="Slider.maxValue"/> to
        /// the same value, then applies the configured <see cref="SliderValueMode"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets both slider endpoints.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets both slider endpoints.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets both slider endpoints.
        /// </summary>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}