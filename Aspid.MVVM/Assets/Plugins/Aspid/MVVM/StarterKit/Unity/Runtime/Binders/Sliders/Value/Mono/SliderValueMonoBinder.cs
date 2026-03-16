using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

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
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        private bool _isNotifyValueChanged = true;
        
        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="Slider.value"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            SetValueInternal(value);
        
        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="Slider.value"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            SetValueInternal(value);
        
        /// <summary>
        /// Sets <see cref="Slider.value"/>, applying the configured converter if present.
        /// Suppresses value change events during assignment.
        /// </summary>
        [BinderLog]
        public void SetValue(float value) =>
            SetValueInternal(value);
        
        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="Slider.value"/>.
        /// </summary>
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

        protected void SetValueInternal(float value)
        {
            _isNotifyValueChanged = false;
            CachedComponent.value = _converter?.Convert(value) ?? value;
            _isNotifyValueChanged = true;
        }

        private void OnValueChanged(float value)
        {
            if (!_isNotifyValueChanged) return;

            IntValueChanged?.Invoke((int)value);
            LongValueChanged?.Invoke((long)value);
            FloatValueChanged?.Invoke(value);
            DoubleValueChanged?.Invoke(value);
        }
    }
}