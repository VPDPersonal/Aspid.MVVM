#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that binds <see cref="Toggle.isOn"/> and reports user changes back.
    /// </summary>
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public sealed class ToggleIsOnBinder : 
        TargetBinder<Toggle>, IBinder<bool>,
        IReverseBinder<bool>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;
        
        [Tooltip("Optional converter applied to the value; reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<bool, bool>? _converter;

        private bool _isSuppressingEcho;

        /// <param name="target">The toggle to bind.</param>
        /// <param name="converter">
        /// The converter applied to the bound value, or <see langword="null"/> to use it as-is; reverse only via
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="notifyMode">
        /// When writes from the ViewModel raise <see cref="Toggle.onValueChanged"/>; user input always does.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public ToggleIsOnBinder(
            Toggle target,
            NotifyMode notifyMode = NotifyMode.Never,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _converter = converter;
            _notifyMode = notifyMode;
        }

        /// <inheritdoc/>
        public event Action<bool>? ValueChanged;

        /// <summary>
        /// Sets <see cref="Toggle.isOn"/> without reporting the write back to the ViewModel.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(bool value)
        {
            var converted = _converter?.Convert(value) ?? value;

            if (!_notifyMode.ShouldNotify(IsBound))
            {
                Target.SetIsOnWithoutNotify(converted);
                return;
            }

            _isSuppressingEcho = true;

            try
            {
                Target.isOn = converted;
            }
            finally
            {
                _isSuppressingEcho = false;
            }
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (!Mode.IsTwo()) return;

            Target.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(Target.isOn);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode.IsTwo())
                Target.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn)
        {
            if (_isSuppressingEcho) return;
            ValueChanged?.Invoke(_converter is ITwoWayConverter<bool, bool> twoWay 
                ? twoWay.ConvertBack(isOn)
                : isOn);
        }
    }
}
