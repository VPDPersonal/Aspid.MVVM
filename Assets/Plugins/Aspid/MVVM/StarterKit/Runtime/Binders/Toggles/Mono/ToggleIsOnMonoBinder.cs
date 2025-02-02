using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggles/Toggle Binder - IsOn")]
    public partial class ToggleIsOnMonoBinder : ComponentMonoBinder<Toggle>, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;
        
        [Header("Parameter")]
        [SerializeField] private BindMode _mode = BindMode.TwoWay;
        
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        private bool _isNotifyValueChanged = true;

        public BindMode Mode => _mode;
        
        [BinderLog]
        public void SetValue(bool value)
        {
            _isNotifyValueChanged = false;
            CachedComponent.isOn = _isInvert ? !value : value;
            _isNotifyValueChanged = true;
        }
        
        protected override void OnBound(in BindParameters parameters, bool isBound)
        {
            if (!isBound) return;
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(CachedComponent.isOn);
        }

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