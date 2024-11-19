using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Toggles
{
    [AddComponentMenu("UI/Binders/Toggles/Toggle Binder")]
    public partial class ToggleMonoBinder : ComponentMonoBinder<Toggle>, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;
        
        [Header("Parameter")]
        [SerializeField] private bool _isInvert;
        [SerializeField] private bool _isReverseEnabled;
        
        private bool _isNotifyValueChanged = true;

        public bool IsReverseEnabled => _isReverseEnabled;

        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            
            CachedComponent.onValueChanged.AddListener(OnValueChanged); 
            _isNotifyValueChanged = true;
        }

        protected override void OnUnbound()
        {
            if (!IsReverseEnabled) return;
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        [BinderLog]
        public void SetValue(bool value)
        {
            value = _isInvert ? !value : value;
            
            if (IsReverseEnabled && CachedComponent.isOn != value)
                _isNotifyValueChanged = false;

            CachedComponent.isOn = value;
        }

        private void OnValueChanged(bool isOn) 
        {
            if (!_isNotifyValueChanged)
            {
                _isNotifyValueChanged = true;
                return;
            }
                
            ValueChanged?.Invoke(_isInvert ? !isOn : isOn);
        }
    }
}