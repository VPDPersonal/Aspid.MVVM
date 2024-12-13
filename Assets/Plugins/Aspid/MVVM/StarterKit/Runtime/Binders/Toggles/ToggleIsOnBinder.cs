#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.ViewModels;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ToggleIsOnBinder : Binder, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool>? ValueChanged;
        
        [Header("Component")]
        [SerializeField] private Toggle _toggle;
        
        [Header("Parameters")]
        [SerializeField] private bool _isInvert;
        [SerializeField] private bool _isReverseEnabled;
        
        private bool _isNotifyValueChanged = true;

        public bool IsReverseEnabled => _isReverseEnabled;

        public ToggleIsOnBinder(Toggle toggle, bool isReverseEnabled = true) 
            : this(toggle, false, isReverseEnabled) { }
        
        public ToggleIsOnBinder(Toggle toggle, bool isInvert, bool isReverseEnabled)
        {
            _isInvert = isInvert;
            _isReverseEnabled = isReverseEnabled;
            _toggle = toggle ?? throw new ArgumentNullException(nameof(toggle));
        }
        
        public void SetValue(bool value)
        {
            _isNotifyValueChanged = false;
            _toggle.isOn = _isInvert ? !value : value;
            _isNotifyValueChanged = true;
        }
        
        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound()
        {
            if (!IsReverseEnabled) return;
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn)
        {
            if (!_isNotifyValueChanged) return;
            ValueChanged?.Invoke(_isInvert ? !isOn : isOn);
        }
    }
}