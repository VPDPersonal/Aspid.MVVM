using System;
using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Unity.Generation;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Toggles
{
    [AddComponentMenu("UI/Binders/Toggles/Toggle Binder")]
    public partial class ToggleMonoBinder : ComponentMonoBinder<Toggle>, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;
        
        [field: Header("Parameter")]
        [field: SerializeField]
        protected bool IsInvert { get; private set; }
        
        [field: SerializeField]
        public bool IsReverseEnabled { get; private set; }

        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            CachedComponent.onValueChanged.AddListener(OnValueChanged); 
        }

        protected override void OnUnbound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.isOn = IsInvert ? !value : value;

        private void OnValueChanged(bool isOn) 
        {
            isOn = IsInvert ? !isOn : isOn;
            ValueChanged?.Invoke(isOn);
        }
    }
}