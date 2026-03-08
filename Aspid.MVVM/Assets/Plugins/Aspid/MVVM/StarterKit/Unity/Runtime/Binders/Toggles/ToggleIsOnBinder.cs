#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A serializable, non-MonoBehaviour binder that binds a boolean ViewModel value to the
    /// <see cref="Toggle.isOn"/> property of a <see cref="Toggle"/> target.
    /// Supports all binding modes. An optional inversion flag allows the logical value to be flipped
    /// before it is applied or propagated back to the source.
    /// </summary>
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public sealed class ToggleIsOnBinder : TargetBinder<Toggle>, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool>? ValueChanged;

        [SerializeField] private bool _isInvert;
        [NonSerialized] private bool _isNotifyValueChanged = true;

        public ToggleIsOnBinder(Toggle target, BindMode mode)
            : this(target, false, mode) { }
        
        public ToggleIsOnBinder(Toggle target, bool isInvert = false, BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _isInvert = isInvert;
        }
        
        public void SetValue(bool value)
        {
            _isNotifyValueChanged = false;
            Target.isOn = _isInvert ? !value : value;
            _isNotifyValueChanged = true;
        }
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            Target.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(Target.isOn);
        }
        
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