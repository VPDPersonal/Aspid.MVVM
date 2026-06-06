using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Toggle}"/> that binds a boolean ViewModel value to <see cref="Toggle.isOn"/>,
    /// supporting all binding modes.
    /// An optional inversion flag flips the logical value before it is applied to the toggle or propagated back to the source.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn")]
    [AddBinderContextMenu(typeof(Toggle), serializePropertyNames: "m_IsOn")]
    [BindModeOverride(IsAll = true)]
    public sealed partial class ToggleIsOnMonoBinder : ComponentMonoBinder<Toggle>,
        IBinder<bool>,
        IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool> ValueChanged;

        [SerializeField] private bool _isInvert;
        [NonSerialized] private bool _isNotifyValueChanged = true;

        /// <summary>
        /// Sets <see cref="Toggle.isOn"/> to the specified value, applying inversion if configured.
        /// </summary>
        [BinderLog]
        public void SetValue(bool value)
        {
            _isNotifyValueChanged = false;
            CachedComponent.isOn = _isInvert ? !value : value;
            _isNotifyValueChanged = true;
        }

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Toggle.onValueChanged"/> when the mode supports it.
        /// </summary>
        /// <remarks>
        /// Subscription is skipped for <see cref="BindMode.OneWay"/>. For <see cref="BindMode.OneWayToSource"/>,
        /// OnValueChanged is invoked immediately to propagate the current toggle state to the source.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            CachedComponent.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(CachedComponent.isOn);
        }

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Toggle.onValueChanged"/> when the mode supports it.
        /// </summary>
        /// <remarks>
        /// Unsubscription is skipped for <see cref="BindMode.OneWay"/>.
        /// </remarks>
        protected override void OnUnbound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn)
        {
            if (!_isNotifyValueChanged) return;
            ValueChanged?.Invoke(_isInvert ? !isOn : isOn);
        }
    }
}