#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Globalization;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_InputField}"/> that binds <see cref="TMP_InputField.text"/>.
    /// Supports <see cref="BindMode.OneWay"/>, <see cref="BindMode.TwoWay"/>, and <see cref="BindMode.OneWayToSource"/>.
    /// Also implements <see cref="INumberBinder"/> and <see cref="IReverseBinder{T}"/>, allowing numeric
    /// formatting and bidirectional text binding.
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
        
        [SerializeReferenceDropdown]
        [Tooltip("Optional converter applied to values before they are set on the input field.")]
        [SerializeReference] private Converter? _converter;
        
        private bool _isNotifyValueChanged = true;
        
        /// <summary>
        /// Initializes a new instance of <see cref="InputFieldBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="converter">The converter applied to values before they are set on the input field, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.None"/>.</param>
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
        public void SetValue(string? value)
        {
            _isNotifyValueChanged = false;
            Target.text = _converter?.Convert(value) ?? value;
            _isNotifyValueChanged = true;
        }

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
        
        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
        
        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
        
        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
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
    }
}
#endif