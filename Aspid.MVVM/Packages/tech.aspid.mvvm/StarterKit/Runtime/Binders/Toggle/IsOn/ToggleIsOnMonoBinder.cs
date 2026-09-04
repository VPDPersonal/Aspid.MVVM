using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that binds <see cref="Toggle.isOn"/> and reports user changes
    /// back.
    /// </summary>
    /// <remarks>
    /// Writes raise <see cref="Toggle.onValueChanged"/> for other listeners; only the binder's own echo is suppressed.
    /// </remarks>
    [BindModeOverride(IsAll = true)]
    [AddBinderContextMenu(typeof(Toggle), serializePropertyNames: "m_IsOn")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn")]
    public sealed partial class ToggleIsOnMonoBinder : ComponentMonoBinder<Toggle>, IBinder<bool>, IReverseBinder<bool>
    {
        [Tooltip("Optional converter applied to the value; reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        [NonSerialized] private bool _isNotifyValueChanged = true;

        /// <inheritdoc/>
        public event Action<bool> ValueChanged;

        /// <summary>
        /// Sets <see cref="Toggle.isOn"/> without reporting the write back to the ViewModel.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value)
        {
            _isNotifyValueChanged = false;

            try
            {
                CachedComponent.isOn = _converter?.Convert(value) ?? value;
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

            CachedComponent.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(CachedComponent.isOn);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
                CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn)
        {
            if (!_isNotifyValueChanged) return;
            ValueChanged?.Invoke(_converter is ITwoWayConverter<bool, bool> twoWay ? twoWay.ConvertBack(isOn) : isOn);
        }
    }
}
