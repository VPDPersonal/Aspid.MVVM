using System;
using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Toggles
{
    [AddComponentMenu("UI/Binders/Toggles/Toggle Binder")]
    public partial class ToggleBinder : ToggleBinderBase, IBinder<bool>, IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;
        
        [Header("Parameters")]
        [SerializeField] private bool _isReverse;
        
#if UNITY_EDITOR
        private bool _isSubscribed;
#endif
        
        public bool IsReverseEnabled => _isReverse;

        private void OnValidate()
        {
            if (!_isReverse) Unsubscribe();
            else if (!_isSubscribed) Subscribe();
        }

        private void OnEnable() => Subscribe();

        private void OnDisable() => Unsubscribe();

        private void Subscribe()
        {
            if (!IsReverseEnabled) return;
#if UNITY_EDITOR
            _isSubscribed = true;
#endif
            CachedToggle.onValueChanged.AddListener(OnValueChanged);
        }
        
        private void Unsubscribe()
        {
#if UNITY_EDITOR
            _isSubscribed = false;
#endif
            CachedToggle.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedToggle.isOn = value;

        private void OnValueChanged(bool isOn) =>
            ValueChanged?.Invoke(isOn);
    }
}