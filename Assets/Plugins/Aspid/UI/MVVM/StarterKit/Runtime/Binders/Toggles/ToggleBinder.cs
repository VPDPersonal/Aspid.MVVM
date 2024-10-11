#nullable enable
using System;
using UnityEngine.UI;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.StarterKit.Binders.Toggles
{
    public class ToggleBinder : Binder, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool>? ValueChanged;
        
        private readonly Toggle _toggle;
        private bool _isNotifyValueChanged = true;
        
        public bool IsInvert { get; }
        
        public bool IsReverseEnabled { get; }

        public ToggleBinder(Toggle toggle, bool isReverseEnabled = true) 
            : this(toggle, false, isReverseEnabled) { }
        
        public ToggleBinder(Toggle toggle, bool isInvert, bool isReverseEnabled)
        {
            IsInvert = isInvert;
            IsReverseEnabled = isReverseEnabled;
            _toggle = toggle ?? throw new ArgumentNullException(nameof(toggle));
        }

        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        public void SetValue(bool value)
        {
            value = IsInvert ? !value : value;
            
            if (IsReverseEnabled && _toggle.isOn != value)
                _isNotifyValueChanged = false;
            
            _toggle.isOn = value;
        }

        private void OnValueChanged(bool isOn)
        {
            if (_isNotifyValueChanged)
                ValueChanged?.Invoke(IsInvert ? !isOn : isOn);
            
            _isNotifyValueChanged = true;
        }
    }
}