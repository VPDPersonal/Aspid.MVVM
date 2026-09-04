#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that binds <see cref="Slider.value"/>, also from other numbers, and
    /// reports user changes back as numbers.
    /// </summary>
    /// <remarks>
    /// Writes raise <see cref="Slider.onValueChanged"/> for other listeners; only the binder's own echo is suppressed.
    /// </remarks>
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class SliderValueBinder : TargetBinder<Slider>, IFloatBinder, INumberReverseBinder
    {
        [Tooltip("Optional converter applied to the value; reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<float, float>? _converter;

        private NumberReverseChannel _channel;
        private bool _isNotifyValueChanged = true;

        /// <param name="target">The slider to bind.</param>
        /// <param name="converter">
        /// The converter applied to the bound value, or <see langword="null"/> to use it as-is; reverse only via
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public SliderValueBinder(
            Slider target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _converter = converter;
        }

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
        public void SetValue(float value)
        {
            var slider = Target;
            var converted = _converter?.Convert(value) ?? value;
            var clamped = this.SafeClamp(converted, slider.minValue, slider.maxValue, slider);

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

            Target.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(Target.value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
                Target.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            if (!_isNotifyValueChanged) return;
            _channel.Raise(_converter is ITwoWayConverter<float, float> twoWay ? twoWay.ConvertBack(value) : value);
        }
    }
}
