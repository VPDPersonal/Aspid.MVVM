using System;
using UnityEngine;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Unity.Generation;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Toggles.Mono
{
    [AddComponentMenu("UI/Binders/Toggles/Toggle Binder")]
    public partial class ToggleMonoBinder : ToggleMonoBinderBase, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;
        
        [Header("Parameters")]
        [SerializeField] private bool _isReverse;
        
        public bool IsReverseEnabled => _isReverse;
        
        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            CachedToggle.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound(IViewModel viewModel, string id)
        {
            CachedToggle.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedToggle.isOn = value;

        private void OnValueChanged(bool isOn) =>
            ValueChanged?.Invoke(isOn);
    }
}