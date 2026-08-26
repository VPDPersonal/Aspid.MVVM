using System;
using UnityEngine;
using UnityEngine.UI;
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;

// ReSharper disable once CheckNamespace
// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Slider}"/> that binds <see cref="Slider.value"/>.
    /// Also implements <see cref="INumberBinder"/> and <see cref="INumberReverseBinder"/>, allowing numeric
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
    public class SliderValueMonoBinder : ComponentMonoBinder<Slider>, INumberBinder, INumberReverseBinder
    {
        /// <inheritdoc/>
        public event Action<int> IntValueChanged;
        /// <inheritdoc/>
        public event Action<long> LongValueChanged;
        /// <inheritdoc/>
        public event Action<float> FloatValueChanged;
        /// <inheritdoc/>
        public event Action<double> DoubleValueChanged;

        [Tooltip("Optional converter applied to values before they are set on the slider.")]
        [SerializeReference] private Converter _converter;

        private bool _isNotifyValueChanged = true;
        
        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="Slider.value"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value) =>
            SetValueInternal(value);
        
        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="Slider.value"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(long value) =>
            SetValueInternal(value);
        
        /// <summary>
        /// Sets <see cref="Slider.value"/>, applying the configured converter if present.
        /// Suppresses value change events during assignment.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value) =>
            SetValueInternal(value);
        
        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="Slider.value"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(double value) =>
            SetValueInternal((float)value);
        
        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Slider.onValueChanged"/> when using
        /// <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <remarks>
        /// When <see cref="BindMode.OneWayToSource"/> is active, the current slider value is also immediately
        /// forwarded to the ViewModel to synchronize the source with the current view state.
        /// </remarks>
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

        /// <remarks>
        /// The value written is clamped to the slider's own range first. Unity clamps it anyway, silently, and the
        /// echo guard around the assignment then swallows the <c>onValueChanged</c> that the clamp raises — so the
        /// ViewModel would keep the value it sent while the slider showed a different one, and the two stayed apart
        /// until the next change. When the clamp actually changes the value, the reverse channel is told what the
        /// slider holds. A converter's own effect is not reported back: only the difference the clamp made is.
        /// </remarks>
        /// <param name="value">The value received from the ViewModel.</param>
        protected void SetValueInternal(float value)
        {
            var slider = CachedComponent;
            var converted = _converter?.Convert(value) ?? value;
            var clamped = BinderMath.SafeClamp(converted, slider.minValue, slider.maxValue);

            _isNotifyValueChanged = false;

            try
            {
                slider.value = clamped;
            }
            finally
            {
                // Без finally исключение из сеттера — например, из чужого слушателя onValueChanged —
                // навсегда оставило бы флаг снятым и обесточило канал View → ViewModel.
                _isNotifyValueChanged = true;
            }

            if (!Mathf.Approximately(clamped, converted)) OnValueChanged(clamped);
        }

        private void OnValueChanged(float value)
        {
            if (!_isNotifyValueChanged) return;

            value = GetConvertedBackValue(value);

            IntValueChanged?.Invoke((int)value);
            LongValueChanged?.Invoke((long)value);
            FloatValueChanged?.Invoke(value);
            DoubleValueChanged?.Invoke(value);
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