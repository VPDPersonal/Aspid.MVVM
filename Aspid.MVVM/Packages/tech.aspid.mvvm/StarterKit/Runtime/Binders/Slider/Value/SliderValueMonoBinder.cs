using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that binds <see cref="Slider.value"/>, also from other numbers,
    /// and reports user changes back as numbers.
    /// </summary>
    /// <remarks>
    /// Writes raise <see cref="Slider.onValueChanged"/> for other listeners; only the binder's own echo is suppressed.
    /// </remarks>
    [BindModeOverride(IsAll = true)]
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value")]
    public partial class SliderValueMonoBinder : ComponentMonoBinder<Slider>, IFloatBinder, INumberReverseBinder
    {
        [Tooltip("Optional converter applied to the value; reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<float, float> _converter;

        private NumberReverseChannel _channel;
        private bool _isNotifyValueChanged = true;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Sets <see cref="Slider.value"/> without reporting the write back to the ViewModel.
        /// </summary>
        /// <remarks>
        /// The converted value is clamped to the slider range; the clamped value is reported back only when the
        /// clamp changed it.
        /// </remarks>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            var slider = CachedComponent;
            var converted = _converter?.Convert(value) ?? value;
            var clamped = this.SafeClamp(converted, slider.minValue, slider.maxValue);

            _isNotifyValueChanged = false;

            try
            {
                slider.value = clamped;
            }
            finally
            {
                // Keeps the reverse channel alive when another listener throws.
                _isNotifyValueChanged = true;
            }

            if (!Mathf.Approximately(clamped, converted)) OnValueChanged(clamped);
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            CachedComponent.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(CachedComponent.value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
                CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            if (!_isNotifyValueChanged) return;
            _channel.Raise(_converter is ITwoWayConverter<float, float> twoWay ? twoWay.ConvertBack(value) : value);
        }
    }
}
