#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;
using System.Globalization;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TMP_InputField}"/> that binds <see cref="TMP_InputField.text"/>.
    /// Also implements <see cref="INumberBinder"/> and <see cref="IReverseBinder{T}"/>, allowing numeric
    /// formatting and bidirectional text binding.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.TwoWay"/> and <see cref="BindMode.OneWayToSource"/>: when the configured
    /// input field event fires, the current text is forwarded to the ViewModel through the corresponding events.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – Text")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_Text")]
    [BindModeOverride(IsAll = true)]
    public sealed partial class InputFieldMonoBinder : ComponentMonoBinder<TMP_InputField>, 
        INumberBinder, 
        IBinder<string>,
        INumberReverseBinder,
        IReverseBinder<string>
    { 
        /// <inheritdoc/>
        public event Action<string> ValueChanged;
        
        /// <inheritdoc/>
        public event Action<int> IntValueChanged;
        
        /// <inheritdoc/>
        public event Action<long> LongValueChanged;
        
        /// <inheritdoc/>
        public event Action<float> FloatValueChanged;
        
        /// <inheritdoc/>
        public event Action<double> DoubleValueChanged;
        
        [Tooltip("Determines the culture used when converting numeric values to string.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        [Tooltip("The input field event that triggers ViewModel notifications.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        [Tooltip("Optional converter applied to values before they are set on the input field.")]
        [SerializeReference] private Converter _converter;
        
        private bool _isNotifyValueChanged = true;

        /// <summary>
        /// Re-wires the input field subscriptions after the bind mode is changed in the inspector during play mode.
        /// </summary>
        /// <remarks>
        /// Only while the binder is actually bound. Without that condition it subscribed to an unbound binder as
        /// well, and <c>Unbind</c> returns immediately when the binder is not bound — so <c>OnUnbound</c>, and with
        /// it the unsubscribe, never ran and the listener stayed on the field. Editing the inspector repeatedly
        /// also stacked duplicate subscriptions, since <c>UnityEvent</c> accepts the same listener more than once.
        /// </remarks>
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying || !IsBound) return;

            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
            {
                CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
                CachedComponent.onEndEdit.RemoveListener(OnValueChanged);
                CachedComponent.onSubmit.RemoveListener(OnValueChanged);
                CachedComponent.onSelect.RemoveListener(OnValueChanged);
                CachedComponent.onDeselect.RemoveListener(OnValueChanged);
                
                Subscribe();
            }
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
            if (Mode is BindMode.OneWayToSource) OnValueChanged(CachedComponent.text);
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
        [BinderLog]
        public void SetValue(string value)
        {
            _isNotifyValueChanged = false;

            try
            {
                CachedComponent.text = _converter?.Convert(value) ?? value;
            }
            finally
            {
                // Без finally исключение из сеттера — например, из чужого слушателя onValueChanged —
                // навсегда оставило бы флаг снятым и обесточило канал View → ViewModel.
                _isNotifyValueChanged = true;
            }
        }

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_InputField.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void OnValueChanged(string value)
        {
            if (!_isNotifyValueChanged) return;
            
            ValueChanged?.Invoke(value);

            if (CachedComponent.contentType 
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