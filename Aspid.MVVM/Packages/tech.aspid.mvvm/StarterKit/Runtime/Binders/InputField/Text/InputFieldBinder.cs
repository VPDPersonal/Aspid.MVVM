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

        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("When writes from the ViewModel raise the field's events; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;
        
        [Tooltip("Optional converter applied to the text; empty leaves it as-is.")]
        [SerializeReference] private IConverter<string?, string?>? _converter;

        private bool _isSuppressingEcho;
        private NumberReverseChannel _channel;

        /// <inheritdoc/>
        public event Action<string?>? ValueChanged;

        /// <param name="target">The field to bind.</param>
        /// <param name="notifyMode"> When writes from the ViewModel raise the field's events; user input always does. </param>
        /// <param name="converter">  The converter applied to the text, or <see langword="null"/> to use it as-is. </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public InputFieldBinder(
            TMP_InputField target,
            NotifyMode notifyMode = NotifyMode.Always,
            IConverter<string?, string?>? converter = null,
            BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            
            _converter = converter;
            _notifyMode = notifyMode;
        }

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (!Mode.IsTwo()) return;
            
            Target.GetEvent(_updateEvent)
                .AddListener(OnValueChanged);
            
            if (Mode is BindMode.OneWayToSource)
                OnValueChanged(Target.text);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode.IsTwo())
            {
                Target.GetEvent(_updateEvent)
                    .RemoveListener(OnValueChanged);
            }
        }

        /// <summary>
        /// Sets <see cref="TMP_InputField.text"/> without notifying the ViewModel back.
        /// </summary>
        /// <remarks>
        /// <see cref="NotifyMode"/> sets whether the write raises <see cref="TMP_InputField.onValueChanged"/>
        /// for other listeners.
        /// </remarks>
        /// <param name="value">The text received from the ViewModel.</param>
        public void SetValue(string? value)
        {
            var converted = _converter?.Convert(value) ?? value;

            if (!_notifyMode.ShouldNotify(IsBound))
            {
                Target.SetTextWithoutNotify(converted);
                return;
            }

            _isSuppressingEcho = true;

            try
            {
                Target.text = converted;
            }
            finally
            {
                _isSuppressingEcho = false;
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

        private void OnValueChanged(string? value)
        {
            if (_isSuppressingEcho) return;

            value = GetConvertedBackValue(value);
            ValueChanged?.Invoke(value);
            Target.RaiseNumber(ref _channel, value, _cultureInfoMode);
        }

        private string? GetConvertedBackValue(string? value) => _converter is ITwoWayConverter<string?, string?> twoWay 
            ? twoWay.ConvertBack(value)
            : value;
    }
}
