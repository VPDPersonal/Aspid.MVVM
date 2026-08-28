using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Slider}"/> that binds <see cref="Slider.value"/>.
    /// Also implements <see cref="IFloatBinder"/> and <see cref="INumberReverseBinder"/>, allowing numeric
    /// values of multiple types to be both pushed to and received from the slider.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.TwoWay"/> and <see cref="BindMode.OneWayToSource"/>: when
    /// <see cref="Slider.onValueChanged"/> fires, the current value is forwarded to the ViewModel.
    /// When <see cref="BindMode.OneWayToSource"/> is active, the current value is also immediately
    /// forwarded when binding is established.
    /// </remarks>
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value")]
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value")]
    public partial class SliderValueMonoBinder : ComponentMonoBinder<Slider>, 
        IFloatBinder,
        INumberReverseBinder
    {
        [Tooltip("Optional converter applied to values before they are set on the slider.")]
        [SerializeReference] private IConverter<float, float> _converter;

        private NumberReverseChannel _channel;
        private bool _isNotifyValueChanged = true;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;
        
        /// <summary>
        /// Sets <see cref="Slider.value"/>, applying the configured converter if present.
        /// Suppresses value change events during assignment.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value) =>
            SetValueInternal(value);
 
        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Slider.onValueChanged"/> when using
        /// <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(CachedComponent.value);
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
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <summary>
        /// Sets <see cref="Slider.value"/>, applying the configured converter if present.
        /// Suppresses value change events during assignment.
        /// </summary>
        /// <remarks>
        /// The value is clamped to the slider's own range before assignment. Unity would clamp it anyway, but
        /// silently, and the echo guard would swallow the <c>onValueChanged</c> the clamp raises — leaving the
        /// ViewModel out of sync until the next change. When the clamp changes the value, the difference is
        /// reported back; a converter's own effect is not.
        /// </remarks>
        /// <param name="value">The value received from the ViewModel.</param>
        protected void SetValueInternal(float value)
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
        /// A one-way converter cannot be undone, so the raw value is the only honest answer — and it
        /// must not be the forward-converted one, which would write the View's presentation back
        /// into the ViewModel.
        /// </remarks>
        private float GetConvertedBackValue(float value) =>
            _converter is ITwoWayConverter<float, float> twoWay ? twoWay.ConvertBack(value) : value;

    }
}