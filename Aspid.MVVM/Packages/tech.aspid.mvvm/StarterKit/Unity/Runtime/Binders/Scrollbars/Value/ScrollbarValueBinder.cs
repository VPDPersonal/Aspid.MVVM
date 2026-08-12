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
    /// <see cref="TargetBinder{Scrollbar}"/> that binds <see cref="Scrollbar.value"/>.
    /// Supports <see cref="BindMode.OneWay"/>, <see cref="BindMode.TwoWay"/>, and <see cref="BindMode.OneWayToSource"/>.
    /// Also implements <see cref="INumberBinder"/> and <see cref="INumberReverseBinder"/>, allowing numeric
    /// values of multiple types to be both pushed to and received from the scrollbar.
    /// </summary>
    /// <remarks>
    /// Unlike <see cref="Slider"/>, a scrollbar has no configurable range: its value is always normalised to
    /// 0..1, so the incoming value is clamped to that range rather than to inspector-set bounds.
    /// </remarks>
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class ScrollbarValueBinder : TargetBinder<Scrollbar>, INumberBinder, INumberReverseBinder
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

        [Tooltip("Optional converter applied to values before they are set on the scrollbar.")]
        [SerializeReference] private Converter? _converter;

        /// <inheritdoc/>
        public ScrollbarValueBinder(Scrollbar target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ScrollbarValueBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Scrollbar"/> to bind.</param>
        /// <param name="converter">The converter applied to values before they are set on the scrollbar, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.None"/>.</param>
        public ScrollbarValueBinder(Scrollbar target, Converter? converter = null, BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _converter = converter;
        }

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="Scrollbar.value"/>.
        /// </summary>
        public void SetValue(int value) =>
            SetValueInternal(value);

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="Scrollbar.value"/>.
        /// </summary>
        public void SetValue(long value) =>
            SetValueInternal(value);

        /// <summary>
        /// Sets <see cref="Scrollbar.value"/>, applying the configured converter if present.
        /// Suppresses value change events during assignment.
        /// </summary>
        public void SetValue(float value) =>
            SetValueInternal(value);

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets <see cref="Scrollbar.value"/>.
        /// </summary>
        public void SetValue(double value) =>
            SetValueInternal((float)value);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Scrollbar.onValueChanged"/> when using
        /// <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <remarks>
        /// When <see cref="BindMode.OneWayToSource"/> is active, the current scrollbar value is also immediately
        /// forwarded to the ViewModel to synchronize the source with the current view state.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            Target.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(Target.value);
        }

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Scrollbar.onValueChanged"/> if active.
        /// </summary>
        protected override void OnUnbound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            Target.onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <summary>
        /// Applies <paramref name="value"/> to the scrollbar without reading the write back as user input.
        /// </summary>
        /// <remarks>
        /// The value is clamped to 0..1 first, and the reverse channel is told whenever the clamp changed it —
        /// otherwise the ViewModel would keep a value the scrollbar never held. A converter's own effect is not
        /// reported back: only the difference the clamp made is.
        /// </remarks>
        protected void SetValueInternal(float value)
        {
            var converted = _converter?.Convert(value) ?? value;
            var clamped = BinderMath.SafeClamp01(converted);

            _isNotifyValueChanged = false;

            try
            {
                Target.value = clamped;
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

            IntValueChanged?.Invoke((int)value);
            LongValueChanged?.Invoke((long)value);
            FloatValueChanged?.Invoke(value);
            DoubleValueChanged?.Invoke(value);
        }
    }
}
