using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn")]
    [AddBinderContextMenu(typeof(Toggle), serializePropertyNames: "m_IsOn")]
    public partial class ToggleIsOnMonoBinder : ComponentMonoBinder<Toggle>, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;
        
        [SerializeField] private bool _isInvert;
      
        [NonSerialized] 
        private bool _isNotifyValueChanged = true;
        
        [BinderLog]
        public void SetValue(bool value)
        {
            _isNotifyValueChanged = false;
            CachedComponent.isOn = _isInvert ? !value : value;
            _isNotifyValueChanged = true;
        }
        
        protected override void OnBound()
        {
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