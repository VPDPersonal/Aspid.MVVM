#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_InputField}"/> that binds <see cref="TMP_InputField.text"/>.
    /// </summary>
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class InputFieldBinder : TargetBinder<TMP_InputField>, IBinder<string?>, INumberBinder, IReverseBinder<string>, INumberReverseBinder
    {
        [Tooltip("Determines the culture used when converting numeric values to string.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        [Tooltip("The input field event that triggers ViewModel notifications.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        [Tooltip("Optional converter applied before setting the input field text.")]
        [SerializeReference] private IConverter<string?, string?>? _converter;

        private NumberReverseChannel _channel;
        private bool _isNotifyValueChanged = true;

        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="converter">The converter applied to values before they are set on the input field, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.None"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public InputFieldBinder(TMP_InputField target, IConverter<string?, string?>? converter = null, BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _converter = converter;
        }

        /// <inheritdoc/>
        public event Action<string>? ValueChanged;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Called when the binder is bound. Subscribes to the configured input field event when using
        /// <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <remarks>
        /// When <see cref="BindMode.OneWayToSource"/> is active, the current text is also immediately
        /// forwarded to the ViewModel to synchronize the source with the current view state.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            
            Subscribe();
            if (Mode is BindMode.OneWayToSource) OnValueChanged(Target.text);
        }

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from the input field event if active.
        /// </summary>
        /// <remarks>
        /// Has no effect when <see cref="BindMode.OneWay"/> is active, since no event subscription
        /// was made during binding.
        /// </remarks>
        protected override void OnUnbound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            Unsubscribe();
        }

        /// <summary>
        /// Sets <see cref="TMP_InputField.text"/>, applying the configured converter if present.
        /// Suppresses value change events during assignment.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(string? value)
        {
            _isNotifyValueChanged = false;

            try
            {
                Target.text = _converter?.Convert(value) ?? value;
            }
            finally
            {
                // finally guards against a listener exception (e.g. from onValueChanged) leaving the flag stuck off.
                _isNotifyValueChanged = true;
            }
        }

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void OnValueChanged(string value)
        {
            if (!_isNotifyValueChanged) return;

            value = GetConvertedBackValue(value);
            ValueChanged?.Invoke(value);

            if (Target.contentType 
                is not (TMP_InputField.ContentType.IntegerNumber 
                or TMP_InputField.ContentType.DecimalNumber)) return;
           
            if (!_channel.HasIntegerListeners && !_channel.HasFloatingPointListeners) return;

            var culture = _cultureInfoMode.ToCultureInfo();
            if (!double.TryParse(value, NumberStyles.Any, culture, out var number)) return;

            if (_channel.HasIntegerListeners)
            {
                // Parsed as a long when it can be: a double holds no long past 2^53 exactly, and the long
                // channel is there for the numbers that need those bits.
                if (long.TryParse(value, NumberStyles.Any, culture, out var integerValue))
                    _channel.RaiseIntegers(integerValue);
                else _channel.RaiseIntegers(number);
            }

            if (_channel.HasFloatingPointListeners) _channel.RaiseFloatingPoint(number);
        }

        /// <summary>
        /// Converts a value on its way back to the ViewModel.
        /// </summary>
        /// <param name="value">The text read from the field.</param>
        /// <returns>
        /// The text as the ViewModel expects it: undone by the converter when it offers
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>, and unchanged when it does not.
        /// </returns>
        /// <remarks>
        /// The numeric channels parse the same converted-back text this method returns.
        /// </remarks>
        private string? GetConvertedBackValue(string? value) =>
            _converter is ITwoWayConverter<string?, string?> twoWay ? twoWay.ConvertBack(value) : value;
    }
}