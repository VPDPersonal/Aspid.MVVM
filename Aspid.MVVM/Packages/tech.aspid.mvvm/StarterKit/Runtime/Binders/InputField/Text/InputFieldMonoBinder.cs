using TMPro;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that binds <see cref="TMP_InputField.text"/>, also from numbers,
    /// and reports edits back as text and, for numeric fields, as numbers.
    /// </summary>
    [BindModeOverride(IsAll = true)]
    [AddBinderContextMenu(
        type: typeof(TMP_InputField),
        serializePropertyNames: "m_Text")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – Text")]
    public sealed partial class InputFieldMonoBinder : ComponentMonoBinder<TMP_InputField>,
        IBinder<string>,
        INumberBinder,
        IReverseBinder<string>,
        INumberReverseBinder
    {
        [Tooltip("Culture numbers are formatted with.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        [Tooltip("Field event that notifies the ViewModel.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        [Tooltip("When writes from the ViewModel raise the field's events; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Always;
        
        [Tooltip("Optional converter applied to the text; empty leaves it as-is.")]
        [SerializeReference] private IConverter<string, string> _converter;

        private bool _isSuppressingEcho;
        private NumberReverseChannel _channel;

        /// <inheritdoc/>
        public event Action<string> ValueChanged;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Re-subscribes to the selected event after an Inspector change in Play mode.
        /// </summary>
        /// <remarks>
        /// Runs only while bound, so the listener is never stacked or left behind.
        /// </remarks>
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying || !IsBound) return;

            CachedComponent.RemoveListenerFromAll(OnValueChanged);

            if (Mode.IsTwo())
            {
                CachedComponent.GetEvent(_updateEvent)
                    .AddListener(OnValueChanged);
            }
        }

        /// <summary>
        /// Called by Unity when the component is added or reset in the Editor to set <see cref="NotifyMode.Never"/>.
        /// </summary>
        /// <remarks>
        /// Existing components keep <see cref="NotifyMode.Always"/>, the value of the field before it was serialized.
        /// </remarks>
        protected override void Reset()
        {
            base.Reset();
            _notifyMode = NotifyMode.Never;
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (!Mode.IsTwo()) return;

            CachedComponent.GetEvent(_updateEvent)
                .AddListener(OnValueChanged);
            
            if (Mode is BindMode.OneWayToSource)
                OnValueChanged(CachedComponent.text);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode.IsTwo())
            {
                CachedComponent.GetEvent(_updateEvent)
                    .RemoveListener(OnValueChanged);
            }
        }

        /// <summary>
        /// Sets <see cref="TMP_InputField.text"/> without notifying the ViewModel back.
        /// </summary>
        /// <remarks>
        /// <see cref="NotifyMode"/> sets whether the write raises <see cref="TMP_InputField.onValueChanged"/> for other
        /// listeners.
        /// </remarks>
        /// <param name="value">The text received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(string value)
        {
            var converted = _converter?.Convert(value) ?? value;

            if (!_notifyMode.ShouldNotify(IsBound))
            {
                CachedComponent.SetTextWithoutNotify(converted);
                return;
            }

            _isSuppressingEcho = true;

            try
            {
                CachedComponent.text = converted;
            }
            finally
            {
                _isSuppressingEcho = false;
            }
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        private void OnValueChanged(string value)
        {
            if (_isSuppressingEcho) return;

            value = GetConvertedBackValue(value);
            ValueChanged?.Invoke(value);
            CachedComponent.RaiseNumber(ref _channel, value, _cultureInfoMode);
        }

        private string GetConvertedBackValue(string value) => _converter is ITwoWayConverter<string, string> twoWay 
            ? twoWay.ConvertBack(value)
            : value;
    }
}
