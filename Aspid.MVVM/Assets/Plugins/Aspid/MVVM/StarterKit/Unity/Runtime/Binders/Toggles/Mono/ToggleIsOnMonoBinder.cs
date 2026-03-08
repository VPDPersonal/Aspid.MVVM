using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A MonoBehaviour binder that binds a boolean ViewModel value to the <see cref="Toggle.isOn"/> property of a <see cref="Toggle"/> target.
    /// Supports all binding modes, including two-way and one-way-to-source.
    /// An optional inversion flag allows the logical value to be flipped before it is applied.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn")]
    [AddBinderContextMenu(typeof(Toggle), serializePropertyNames: "m_IsOn")]
    [BindModeOverride(IsAll = true)]
    public sealed partial class ToggleIsOnMonoBinder : ComponentMonoBinder<Toggle>,
        IBinder<bool>,
        IReverseBinder<bool>
    {
        public event Action<bool> ValueChanged;

        [SerializeField] private bool _isInvert;
        [NonSerialized] private bool _isNotifyValueChanged = true;

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