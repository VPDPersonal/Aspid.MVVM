#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Globalization;
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_InputField}"/> that binds <see cref="TMP_InputField.text"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-Text-1.1.0.xml" path="doc//member[@name='InputFieldBinder']/*" />
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class InputFieldBinder : TargetBinder<TMP_InputField>, IBinder<string?>, INumberBinder, IReverseBinder<string>, INumberReverseBinder
    {
        /// <inheritdoc/>
        public event Action<string>? ValueChanged;
        
        /// <inheritdoc/>
        public event Action<int>? IntValueChanged;
        
        /// <inheritdoc/>
        public event Action<long>? LongValueChanged;
        
        /// <inheritdoc/>
        public event Action<float>? FloatValueChanged;
        
        /// <inheritdoc/>
        public event Action<double>? DoubleValueChanged;
     
        [Tooltip("Determines the culture used when converting numeric values to string.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        [Tooltip("The input field event that triggers ViewModel notifications.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        
        [Tooltip("Optional converter applied before setting the input field text.")]
        [SerializeReference] private Converter? _converter;
        
        private bool _isNotifyValueChanged = true;
        
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="converter">The converter applied to values before they are set on the input field, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.None"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public InputFieldBinder(TMP_InputField target, Converter? converter = null, BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _converter = converter;
        }

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
           
            if (IntValueChanged != null || LongValueChanged != null)
            {
                if (!long.TryParse(value, NumberStyles.Any, _cultureInfoMode.ToCultureInfo(), out var integerValue)) return;

                if (integerValue is <= int.MaxValue and >= int.MinValue)
                    IntValueChanged?.Invoke((int)integerValue);

                LongValueChanged?.Invoke(integerValue);
            }
            
            if (FloatValueChanged != null || DoubleValueChanged != null)
            {
                if (!double.TryParse(value, NumberStyles.Any, _cultureInfoMode.ToCultureInfo(), out var decimalValue)) return;
                
                if (decimalValue is <= float.MaxValue and >= float.MinValue)
                    FloatValueChanged?.Invoke((float)decimalValue);

                DoubleValueChanged?.Invoke(decimalValue);
            }
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
        /// A one-way converter cannot be undone, so the raw text is the only honest answer — and it
        /// must not be the forward-converted one, which would write the View's presentation back
        /// into the ViewModel. The numeric channels read the same converted-back text, so a field
        /// whose converter strips a currency symbol parses the number underneath it.
        /// </remarks>
        private string? GetConvertedBackValue(string? value) =>
            _converter is ITwoWayConverter<string?, string?> twoWay ? twoWay.ConvertBack(value) : value;

    }
}
#endif