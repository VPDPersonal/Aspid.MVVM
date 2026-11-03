using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that binds <see cref="Toggle.isOn"/> and reports user changes
    /// back.
    /// </summary>
    [BindModeOverride(IsAll = true)]
    [AddBinderContextMenu(
        type: typeof(Toggle),
        serializePropertyNames: "m_IsOn")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn")]
    public sealed partial class ToggleIsOnMonoBinder :
        ComponentMonoBinder<Toggle>,
        IBinder<bool>,
        IReverseBinder<bool>
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Always;
        
        [Tooltip("Optional converter applied to the value; reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        private bool _isSuppressingEcho;

        /// <inheritdoc/>
        public event Action<bool> ValueChanged;

        /// <summary>
        /// Sets <see cref="Toggle.isOn"/> without reporting the write back to the ViewModel.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value)
        {
            var converted = _converter?.Convert(value) ?? value;

            if (!_notifyMode.ShouldNotify(IsBound))
            {
                CachedComponent.SetIsOnWithoutNotify(converted);
                return;
            }

            _isSuppressingEcho = true;

            try
            {
                CachedComponent.isOn = converted;
            }
            finally
            {
                _isSuppressingEcho = false;
            }
        }

        /// <summary>
        /// Called by Unity when the component is added or reset in the Editor to set <see cref="NotifyMode.Never"/>.
        /// </summary>
        /// <remarks>
        /// Existing components keep <see cref="NotifyMode.Always"/>, the value of the field before it was serialized.
        /// </remarks>
        protected override void Reset()
        {
            base.Reset();
            _notifyMode = NotifyMode.Never;
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (!Mode.IsTwo()) return;

            CachedComponent.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource)
                OnValueChanged(CachedComponent.isOn);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode.IsTwo())
                CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn)
        {
            if (_isSuppressingEcho) return;
            ValueChanged?.Invoke(_converter is ITwoWayConverter<bool, bool> twoWay 
                ? twoWay.ConvertBack(isOn)
                : isOn);
        }
    }
}
