using System;
using UnityEngine.UI;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Toggles
{
    public class ToggleBinder : ToggleBinderBase, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;
        
        public bool IsReverseEnabled { get; }

        public ToggleBinder(Toggle toggle, bool isReverseEnabled = true) : base(toggle)
        {
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