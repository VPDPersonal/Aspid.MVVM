using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UI/Toggles/Toggle Binder - IsOn")]
    public partial class ToggleIsOnMonoBinder : ComponentMonoBinder<Toggle>, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;
        
        [Header("Parameter")]
        [SerializeField] private bool _isReverseEnabled = true;
        
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        private bool _isNotifyValueChanged = true;

        public bool IsReverseEnabled => _isReverseEnabled;
        
        [BinderLog]
        public void SetValue(bool value)
        {
            _isNotifyValueChanged = false;
            CachedComponent.isOn = _isInvert ? !value : value;
            _isNotifyValueChanged = true;
        }
        
        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            CachedComponent.onValueChanged.AddListener(OnValueChanged); 
        }

        protected override void OnUnbound()
        {
            if (!IsReverseEnabled) return;
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn) 
        {
            if (!_isNotifyValueChanged) return;
            ValueChanged?.Invoke(_isInvert ? !isOn : isOn);
        }
    }
}