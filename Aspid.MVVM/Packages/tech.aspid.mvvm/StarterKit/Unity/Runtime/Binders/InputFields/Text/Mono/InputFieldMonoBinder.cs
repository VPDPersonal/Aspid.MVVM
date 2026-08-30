#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TMP_InputField}"/> that binds <see cref="TMP_InputField.text"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – Text")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_Text")]
    [BindModeOverride(IsAll = true)]
    public sealed partial class InputFieldMonoBinder : ComponentMonoBinder<TMP_InputField>, 
        INumberBinder, 
        IBinder<string>,
        INumberReverseBinder,
        IReverseBinder<string>
    { 
        [Tooltip("Determines the culture used when converting numeric values to string.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        [Tooltip("The input field event that triggers ViewModel notifications.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        [Tooltip("Optional converter applied before setting the input field text.")]
        [SerializeReference] private IConverter<string, string> _converter;

        private NumberReverseChannel _channel;
        private bool _isNotifyValueChanged = true;

        /// <inheritdoc/>
        public event Action<string> ValueChanged;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Re-wires the input field subscriptions after the bind mode is changed in the inspector during play mode.
        /// </summary>
        /// <remarks>
        /// Only rewires while the binder is bound. <c>UnityEvent</c> accepts the same listener more than once, so
        /// resubscribing unconditionally would stack duplicate listeners on repeated inspector edits.
        /// </remarks>
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying || !IsBound) return;

            // Removed unconditionally: switching Mode away from TwoWay/OneWayToSource in the inspector must
            // also drop the listener registered under the old mode, or it stays subscribed forever.
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            CachedComponent.onEndEdit.RemoveListener(OnValueChanged);
            CachedComponent.onSubmit.RemoveListener(OnValueChanged);
            CachedComponent.onSelect.RemoveListener(OnValueChanged);
            CachedComponent.onDeselect.RemoveListener(OnValueChanged);

            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource) Subscribe();
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
                // finally guards against a listener exception (e.g. from onValueChanged) leaving the flag stuck off.
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

            value = GetConvertedBackValue(value);
            ValueChanged?.Invoke(value);

            if (CachedComponent.contentType 
                is not (TMP_InputField.ContentType.IntegerNumber 
                or TMP_InputField.ContentType.DecimalNumber)) return;
           
            if (!_channel.HasIntegerListeners && !_channel.HasDecimalListeners) return;

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

            if (_channel.HasDecimalListeners) _channel.RaiseDecimals(number);
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
#endif