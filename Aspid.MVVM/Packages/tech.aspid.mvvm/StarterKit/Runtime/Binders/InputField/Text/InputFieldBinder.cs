#nullable enable
using TMPro;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that binds <see cref="TMP_InputField.text"/>, also from numbers, and reports
    /// edits back as text and, for numeric fields, as numbers.
    /// </summary>
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class InputFieldBinder : TargetBinder<TMP_InputField>,
        IBinder<string?>,
        INumberBinder,
        IReverseBinder<string>,
        INumberReverseBinder
    {
        [Tooltip("Culture numbers are formatted with.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        [Tooltip("Field event that notifies the ViewModel.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        [Tooltip("Optional converter applied to the text; empty leaves it as-is.")]
        [SerializeReference] private IConverter<string?, string?>? _converter;

        private NumberReverseChannel _channel;
        private bool _isNotifyValueChanged = true;

        /// <param name="target">The field to bind.</param>
        /// <param name="converter">
        /// The converter applied to the text, or <see langword="null"/> to use it as-is.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public InputFieldBinder(
            TMP_InputField target,
            IConverter<string?, string?>? converter = null,
            BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _converter = converter;
        }

        /// <inheritdoc/>
        public event Action<string?>? ValueChanged;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            Target.GetEvent(_updateEvent).AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(Target.text);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
                Target.GetEvent(_updateEvent).RemoveListener(OnValueChanged);
        }

        /// <summary>
        /// Sets <see cref="TMP_InputField.text"/> without notifying the ViewModel back.
        /// </summary>
        /// <param name="value">The text received from the ViewModel.</param>
        public void SetValue(string? value)
        {
            _isNotifyValueChanged = false;

            try
            {
                Target.text = _converter?.Convert(value) ?? value;
            }
            finally
            {
                _isNotifyValueChanged = true;
            }
        }

        /// <inheritdoc/>
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        private void OnValueChanged(string value)
        {
            if (!_isNotifyValueChanged) return;

            value = GetConvertedBackValue(value);
            ValueChanged?.Invoke(value);
            Target.RaiseNumber(ref _channel, value, _cultureInfoMode);
        }

        private string? GetConvertedBackValue(string? value) => _converter is ITwoWayConverter<string?, string?> twoWay 
            ? twoWay.ConvertBack(value)
            : value;
    }
}
