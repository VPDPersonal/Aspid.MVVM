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
    /// Writes raise <see cref="Scrollbar.onValueChanged"/> for other listeners; only the binder's own echo is
    /// suppressed.
    /// </remarks>
    [GenerateSerializableBinder]
    [BindModeOverride(IsAll = true)]
    [AddBinderContextMenu(typeof(Scrollbar), serializePropertyNames: "m_Value")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value")]
    public partial class ScrollbarValueMonoBinder : ComponentMonoBinder<Scrollbar>, IFloatBinder, INumberReverseBinder
    {
        [Tooltip("Optional converter applied to the value; reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<float, float> _converter;

        private NumberReverseChannel _channel;
        private bool _isNotifyValueChanged = true;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Sets <see cref="Scrollbar.value"/> without reporting the write back to the ViewModel.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            var converted = _converter?.Convert(value) ?? value;
            var clamped = this.SafeClamp01(converted);

            _isNotifyValueChanged = false;

            try
            {
                CachedComponent.value = clamped;
            }
            finally
            {
                // Keeps the reverse channel alive when another listener throws.
                _isNotifyValueChanged = true;
            }

            if (!Mathf.Approximately(clamped, converted)) OnValueChanged(clamped);
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            CachedComponent.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(CachedComponent.value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
                CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            if (!_isNotifyValueChanged) return;
            _channel.Raise(_converter is ITwoWayConverter<float, float> twoWay ? twoWay.ConvertBack(value) : value);
        }
    }
}
