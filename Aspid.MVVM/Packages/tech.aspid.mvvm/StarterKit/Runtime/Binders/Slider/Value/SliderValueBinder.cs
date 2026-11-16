#if ASPID_MVVM_HAS_UGUI
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

        private uint _generation;
        private bool _isSuppressingEcho;
        private bool _hasPendingNormalization;
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
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> is <see cref="BindMode.None"/> or undefined.</exception>
        public SliderValueBinder(
            Slider target,
            NotifyMode notifyMode = NotifyMode.Never,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.EnsureValid();

            _converter = converter;
            _notifyMode = notifyMode;
        }

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Sets <see cref="Slider.value"/> without reporting the write back to the ViewModel.
        /// </summary>
        /// <remarks>
        /// TwoWay reports the applied value back when clamping or whole-number rounding changes it;
        /// the initial correction waits until binding completes.
        /// </remarks>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(float value)
        {
            var generation = _generation;
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

            if (generation != _generation) return;

            var applied = slider.value;
            var needsNormalization = !Mathf.Approximately(applied, converted);

            if (!IsBound)
                _hasPendingNormalization = Mode is BindMode.TwoWay && needsNormalization;

            if (Mode is BindMode.TwoWay && needsNormalization)
                OnValueChanged(applied);
        }

        /// <inheritdoc/>
        protected override void OnBinding()
        {
            unchecked { _generation++; }
            _hasPendingNormalization = false;
        }

        /// <inheritdoc/>
        protected override void OnAttach()
        {
            if (!Mode.IncludesReverse()) return;
            Target.onValueChanged.AddListener(OnValueChanged);
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            var normalize = _hasPendingNormalization;
            _hasPendingNormalization = false;

            if (Mode is BindMode.OneWayToSource || Mode is BindMode.TwoWay && normalize)
                OnValueChanged(Target.value);
        }

        /// <inheritdoc/>
        protected override void OnDetach()
        {
            unchecked { _generation++; }
            _hasPendingNormalization = false;
            if (Mode.IncludesReverse())
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
#endif
