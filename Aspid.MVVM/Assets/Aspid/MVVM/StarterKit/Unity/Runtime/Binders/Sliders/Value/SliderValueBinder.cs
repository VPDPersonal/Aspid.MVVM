#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Slider}"/> that binds <see cref="Slider.value"/>.
    /// Supports <see cref="BindMode.OneWay"/>, <see cref="BindMode.TwoWay"/>, and <see cref="BindMode.OneWayToSource"/>.
    /// Also implements <see cref="INumberBinder"/> and <see cref="INumberReverseBinder"/>, allowing numeric
    /// values of multiple types to be both pushed to and received from the slider.
    /// </summary>
    /// <include file="XmlExampleDoc-Slider-Value-1.1.0.xml" path="doc//member[@name='SliderValueBinder']/*" />
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class SliderValueBinder : TargetBinder<Slider>, INumberBinder, INumberReverseBinder
    {
        /// <inheritdoc/>
        public event Action<int>? IntValueChanged;
        
        /// <inheritdoc/>
        public event Action<long>? LongValueChanged;
        
        /// <inheritdoc/>
        public event Action<float>? FloatValueChanged;
        
        /// <inheritdoc/>
        public event Action<double>? DoubleValueChanged;

        private bool _isNotifyValueChanged = true;

        [Tooltip("Optional converter applied to values before they are set on the slider.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        /// <inheritdoc/>
        public SliderValueBinder(Slider target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="SliderValueBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Slider"/> to bind.</param>
        /// <param name="converter">The converter applied to values before they are set on the slider, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.None"/>.</param>
        public SliderValueBinder(Slider target, Converter? converter = null, BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _converter = converter;
        }
        
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
        /// Casts the value to <see langword="float"/> and sets <see cref="Slider.value"/>.
        /// </summary>
        public void SetValue(int value) =>
            SetValue((float)value);
        
        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="Slider.value"/>.
        /// </summary>
        public void SetValue(long value) =>
            SetValue((float)value);
        
        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="Slider.value"/>.
        /// </summary>
        public void SetValue(double value) =>
            SetValue((float)value);
        
        /// <summary>
        /// Sets <see cref="Slider.value"/>, applying the configured converter if present.
        /// Suppresses value change events during assignment.
        /// </summary>
        public void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;

            _isNotifyValueChanged = false;
            Target.value = value;
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