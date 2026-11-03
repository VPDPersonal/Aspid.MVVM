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
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class SliderValueBinder : TargetBinder<Slider>, IFloatBinder, INumberReverseBinder
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;
        
        [Tooltip("Optional converter applied to the value; reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<float, float>? _converter;

        private bool _isSuppressingEcho;
        private NumberReverseChannel _channel;

        /// <param name="target">The slider to bind.</param>
        /// <param name="notifyMode">
        /// When writes from the ViewModel raise <see cref="Slider.onValueChanged"/>; user input always does.
        /// </param>
        /// <param name="converter">
        /// The converter applied to the bound value, or <see langword="null"/> to use it as-is; reverse only via
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public SliderValueBinder(
            Slider target,
            NotifyMode notifyMode = NotifyMode.Never,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            
            _converter = converter;
            _notifyMode = notifyMode;
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

            if (_notifyMode.ShouldNotify(IsBound))
            {
                _isSuppressingEcho = true;

                try
                {
                    slider.value = clamped;
                }
                finally
                {
                    _isSuppressingEcho = false;
                }
            }
            else
            {
                slider.SetValueWithoutNotify(clamped);
            }

            if (!Mathf.Approximately(clamped, converted)) 
                OnValueChanged(clamped);
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (!Mode.IsTwo()) return;

            Target.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(Target.value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode.IsTwo())
                Target.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            if (_isSuppressingEcho) return;
            
            _channel.Raise(_converter is ITwoWayConverter<float, float> twoWay 
                ? twoWay.ConvertBack(value)
                : value);
        }
    }
}
