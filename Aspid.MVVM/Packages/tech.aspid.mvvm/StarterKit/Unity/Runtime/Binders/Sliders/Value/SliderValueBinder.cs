#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Slider}"/> that binds <see cref="Slider.value"/>.
    /// Also implements <see cref="IFloatBinder"/> and <see cref="INumberReverseBinder"/>, allowing numeric
    /// values of multiple types to be both pushed to and received from the slider.
    /// </summary>
    /// <remarks>
    /// When <see cref="BindMode.OneWayToSource"/> is active, the current value is also immediately
    /// forwarded when binding is established.
    /// </remarks>
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class SliderValueBinder : TargetBinder<Slider>,
        IFloatBinder,
        INumberReverseBinder
    {
        [Tooltip("Optional converter applied to values before they are set on the slider.")]
        [SerializeReference] private IConverter<float, float>? _converter;

        private NumberReverseChannel _channel;
        private bool _isNotifyValueChanged = true;

        /// <inheritdoc/>
        public SliderValueBinder(Slider target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <param name="target">The <see cref="Slider"/> to bind.</param>
        /// <param name="converter">The converter applied to values before they are set on the slider, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.None"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public SliderValueBinder(Slider target, IConverter<float, float>? converter = null, BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _converter = converter;
        }

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Slider.onValueChanged"/> when using
        /// <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <remarks>
        /// When <see cref="BindMode.OneWayToSource"/> is active, the current slider value is also
        /// immediately forwarded to the ViewModel.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            
            Target.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(Target.value);
        }

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Slider.onValueChanged"/> if active.
        /// </summary>
        /// <remarks>
        /// Has no effect when <see cref="BindMode.OneWay"/> is active, since no event subscription
        /// was made during binding.
        /// </remarks>
        protected override void OnUnbound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            Target.onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <summary>
        /// Sets <see cref="Slider.value"/>, applying the configured converter if present.
        /// Suppresses value change events during assignment.
        /// </summary>
        /// <remarks>
        /// The value is clamped to the slider's own range before assignment; the reverse channel is raised
        /// only when the clamp changed it, not when the converter did.
        /// </remarks>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(float value)
        {
            var converted = _converter?.Convert(value) ?? value;
            var clamped = this.SafeClamp(converted, Target.minValue, Target.maxValue, Target);

            _isNotifyValueChanged = false;

            try
            {
                Target.value = clamped;
            }
            finally
            {
                // Without finally, an exception from the setter (e.g. from another onValueChanged listener)
                // would leave the flag stuck false, permanently killing the View -> ViewModel channel.
                _isNotifyValueChanged = true;
            }

            if (!Mathf.Approximately(clamped, converted)) OnValueChanged(clamped);
        }

        private void OnValueChanged(float value)
        {
            if (!_isNotifyValueChanged) return;
            _channel.Raise(GetConvertedBackValue(value));
        }

        /// <summary>
        /// Converts a value on its way back to the ViewModel.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        /// <returns>
        /// The value as the ViewModel expects it: undone by the converter when it offers
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>, and unchanged when it does not.
        /// </returns>
        /// <remarks>
        /// The raw value is returned, not the forward-converted one — applying the forward conversion
        /// again would write the View's presentation back into the ViewModel.
        /// </remarks>
        private float GetConvertedBackValue(float value) =>
            _converter is ITwoWayConverter<float, float> twoWay ? twoWay.ConvertBack(value) : value;
    }
}