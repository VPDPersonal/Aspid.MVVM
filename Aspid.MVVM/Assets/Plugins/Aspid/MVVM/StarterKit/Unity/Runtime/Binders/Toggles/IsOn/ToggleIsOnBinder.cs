#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Toggle}"/> that binds a boolean ViewModel value to <see cref="Toggle.isOn"/>,
    /// supporting all binding modes.
    /// An optional inversion flag flips the logical value before it is applied to the toggle or propagated back to the source.
    /// </summary>
    /// <include file="XmlExampleDoc-Toggle-IsOn-1.1.0.xml" path="doc//member[@name='ToggleIsOnBinder']/*" />
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public sealed class ToggleIsOnBinder : TargetBinder<Toggle>, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool>? ValueChanged;

        [SerializeField] private bool _isInvert;
        [NonSerialized] private bool _isNotifyValueChanged = true;

        /// <inheritdoc/>
        public ToggleIsOnBinder(Toggle target, BindMode mode)
            : this(target, false, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ToggleIsOnBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Toggle"/> to bind.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound value is logically inverted before being applied or propagated.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.None"/>.</param>
        public ToggleIsOnBinder(Toggle target, bool isInvert = false, BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _isInvert = isInvert;
        }

        /// <summary>
        /// Sets <see cref="Toggle.isOn"/> to the specified value, applying inversion if configured.
        /// </summary>
        public void SetValue(bool value)
        {
            _isNotifyValueChanged = false;
            Target.isOn = _isInvert ? !value : value;
            _isNotifyValueChanged = true;
        }
        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Toggle.onValueChanged"/> when the mode supports it.
        /// </summary>
        /// <remarks>
        /// Subscription is skipped for <see cref="BindMode.OneWay"/>. For <see cref="BindMode.OneWayToSource"/>,
        /// <c>OnValueChanged</c> is invoked immediately to propagate the current toggle state to the source.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            Target.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(Target.isOn);
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
            Target.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn)
        {
            if (!_isNotifyValueChanged) return;
            ValueChanged?.Invoke(_isInvert ? !isOn : isOn);
        }
    }
}