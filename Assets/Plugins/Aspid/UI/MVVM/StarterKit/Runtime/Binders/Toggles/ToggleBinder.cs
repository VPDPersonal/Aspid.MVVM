using System;
using UnityEngine.UI;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.StarterKit.Binders.Toggles
{
    public class ToggleBinder : Binder, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;
        
        protected readonly Toggle Toggle;
        
        public bool IsInvert { get; }
        
        public bool IsReverseEnabled { get; }

        public ToggleBinder(Toggle toggle, bool isReverseEnabled = true) 
            : this(toggle, false, isReverseEnabled) { }
        
        public ToggleBinder(Toggle toggle, bool isInvert, bool isReverseEnabled)
        {
            Toggle = toggle;
            IsInvert = isInvert;
            IsReverseEnabled = isReverseEnabled;
        }

        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            Toggle.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound(IViewModel viewModel, string id)
        {
            Toggle.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        public void SetValue(bool value) =>
            Toggle.isOn = value;

        private void OnValueChanged(bool isOn) =>
            ValueChanged?.Invoke(isOn);
    }
}