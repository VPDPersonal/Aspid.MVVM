using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that binds <see cref="Scrollbar.value"/>, also from other numbers,
    /// and reports user changes back as numbers.
    /// </summary>
    /// <remarks>
    /// The converted value is clamped to [0, 1]; the clamped value is reported back only when the clamp changed it.
    /// </remarks>
    [GenerateSerializableBinder]
    [BindModeOverride(IsAll = true)]
    [AddBinderContextMenu(
        type: typeof(Scrollbar),
        serializePropertyNames: "m_Value")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value")]
    public partial class ScrollbarValueMonoBinder : ComponentMonoBinder<Scrollbar>, IFloatBinder, INumberReverseBinder
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Always;
        
        [Tooltip("Optional converter applied to the value; reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<float, float> _converter;

        private bool _isSuppressingEcho;
        private NumberReverseChannel _channel;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

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
        
        /// <summary>
        /// Sets <see cref="Scrollbar.value"/> without reporting the write back to the ViewModel.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            var converted = _converter?.Convert(value) ?? value;
            var clamped = this.SafeClamp01(converted);

            if (_notifyMode.ShouldNotify(IsBound))
            {
                _isSuppressingEcho = true;

                try
                {
                    CachedComponent.value = clamped;
                }
                finally
                {
                    _isSuppressingEcho = false;
                }
            }
            else
            {
                CachedComponent.SetValueWithoutNotify(clamped);
            }

            if (!Mathf.Approximately(clamped, converted))
                OnValueChanged(clamped);
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (!Mode.IsTwo()) return;

            CachedComponent.onValueChanged.AddListener(OnValueChanged);
            
            if (Mode is BindMode.OneWayToSource)
                OnValueChanged(CachedComponent.value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode.IsTwo())
                CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            if (_isSuppressingEcho) return;
            _channel.Raise(_converter is ITwoWayConverter<float, float> twoWay 
                ? twoWay.ConvertBack(value) 
                : value);
        }
    }
}
