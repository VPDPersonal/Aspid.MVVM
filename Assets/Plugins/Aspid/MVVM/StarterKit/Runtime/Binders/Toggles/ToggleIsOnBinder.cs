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
        
        [Header("Parameter")]
        [SerializeField] private bool _isReverseEnabled;
        
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        private bool _isNotifyValueChanged = true;

        public bool IsReverseEnabled => _isReverseEnabled;

        public ToggleIsOnBinder(Toggle target, bool isReverseEnabled = true) 
            : this(target, false, isReverseEnabled) { }
        
        public ToggleIsOnBinder(Toggle target, bool isInvert, bool isReverseEnabled)
            : base(target)
        {
            _isInvert = isInvert;
            _isReverseEnabled = isReverseEnabled;
        }
        
        public void SetValue(bool value)
        {
            _isNotifyValueChanged = false;
            Target.isOn = _isInvert ? !value : value;
            _isNotifyValueChanged = true;
        }
        
        protected override void OnBound(in BindParameters parameters)
        {
            if (!IsReverseEnabled) return;
            Target.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound()
        {
            if (!IsReverseEnabled) return;
            Target.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn)
        {
            if (!_isNotifyValueChanged) return;
            ValueChanged?.Invoke(_isInvert ? !isOn : isOn);
        }
    }
}