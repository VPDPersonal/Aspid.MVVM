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
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_Text")]
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

        [Tooltip("Optional converter applied to the text; empty leaves it as-is.")]
        [SerializeReference] private IConverter<string, string> _converter;

        private NumberReverseChannel _channel;
        private bool _isNotifyValueChanged = true;

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

            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
                CachedComponent.GetEvent(_updateEvent).AddListener(OnValueChanged);
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            CachedComponent.GetEvent(_updateEvent).AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(CachedComponent.text);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
                CachedComponent.GetEvent(_updateEvent).RemoveListener(OnValueChanged);
        }

        /// <summary>
        /// Sets <see cref="TMP_InputField.text"/> without notifying the ViewModel back.
        /// </summary>
        /// <param name="value">The text received from the ViewModel.</param>
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
                _isNotifyValueChanged = true;
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
            if (!_isNotifyValueChanged) return;

            value = GetConvertedBackValue(value);
            ValueChanged?.Invoke(value);
            CachedComponent.RaiseNumber(ref _channel, value, _cultureInfoMode);
        }

        private string GetConvertedBackValue(string value) =>
            _converter is ITwoWayConverter<string, string> twoWay ? twoWay.ConvertBack(value) : value;
    }
}
