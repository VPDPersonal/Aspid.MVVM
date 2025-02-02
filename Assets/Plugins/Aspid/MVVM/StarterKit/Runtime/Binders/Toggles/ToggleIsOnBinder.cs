#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ToggleIsOnBinder : TargetBinder<Toggle>, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool>? ValueChanged;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameter")]
        [SerializeField] private BindMode _mode = BindMode.TwoWay;
        
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        private bool _isNotifyValueChanged = true;

        public BindMode Mode => _mode;
        
        public ToggleIsOnBinder(Toggle target, BindMode mode = BindMode.TwoWay) 
            : this(target, false, mode) { }
        
        public ToggleIsOnBinder(Toggle target, bool isInvert, BindMode mode)
            : base(target)
        {
            _mode = mode;
            _isInvert = isInvert;
        }
        
        public void SetValue(bool value)
        {
            _isNotifyValueChanged = false;
            Target.isOn = _isInvert ? !value : value;
            _isNotifyValueChanged = true;
        }
        
        protected override void OnBound(in BindParameters parameters, bool isBound)
        {
            if (!isBound) return;
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