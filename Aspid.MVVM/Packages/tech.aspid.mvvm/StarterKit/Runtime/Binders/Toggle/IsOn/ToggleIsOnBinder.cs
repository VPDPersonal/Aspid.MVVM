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
    /// <remarks>
    /// Writes raise <see cref="Toggle.onValueChanged"/> for other listeners; only the binder's own echo is suppressed.
    /// </remarks>
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public sealed class ToggleIsOnBinder : TargetBinder<Toggle>, IBinder<bool>, IReverseBinder<bool>
    {
        [Tooltip("Optional converter applied to the value; reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<bool, bool>? _converter;

        [NonSerialized] private bool _isNotifyValueChanged = true;

        /// <param name="target">The toggle to bind.</param>
        /// <param name="converter">
        /// The converter applied to the bound value, or <see langword="null"/> to use it as-is; reverse only via
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.None"/>.</exception>
        public ToggleIsOnBinder(
            Toggle target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _converter = converter;
        }

        /// <inheritdoc/>
        public event Action<bool>? ValueChanged;

        /// <summary>
        /// Sets <see cref="Toggle.isOn"/> without reporting the write back to the ViewModel.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(bool value)
        {
            _isNotifyValueChanged = false;

            try
            {
                Target.isOn = _converter?.Convert(value) ?? value;
            }
            finally
            {
                // Keeps the reverse channel alive when another listener throws.
                _isNotifyValueChanged = true;
            }
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            Target.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(Target.isOn);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
                Target.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn)
        {
            if (!_isNotifyValueChanged) return;
            ValueChanged?.Invoke(_converter is ITwoWayConverter<bool, bool> twoWay ? twoWay.ConvertBack(isOn) : isOn);
        }
    }
}
