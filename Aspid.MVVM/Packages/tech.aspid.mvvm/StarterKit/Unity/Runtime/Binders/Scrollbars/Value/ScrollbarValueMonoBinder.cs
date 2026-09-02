using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Scrollbar}"/> that binds <see cref="Scrollbar.value"/>.
    /// Also implements <see cref="IFloatBinder"/> and <see cref="INumberReverseBinder"/>, allowing numeric
    /// values of multiple types to be both pushed to and received from the scrollbar.
    /// </summary>
    /// <remarks>
    /// A scrollbar has no configurable range: its value is always normalised to 0..1, so the incoming
    /// value is clamped to that range rather than to inspector-set bounds.
    /// </remarks>
    [GenerateSerializableBinder]
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value")]
    [AddBinderContextMenu(typeof(Scrollbar), serializePropertyNames: "m_Value")]
    public partial class ScrollbarValueMonoBinder : ComponentMonoBinder<Scrollbar>, 
        IFloatBinder,
        INumberReverseBinder
    {
        [Tooltip("Optional converter applied to values before they are set on the scrollbar.")]
        [SerializeReference] private IConverter<float, float> _converter;

        private NumberReverseChannel _channel;
        private bool _isNotifyValueChanged = true;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Sets <see cref="Scrollbar.value"/>, applying the configured converter if present.
        /// Suppresses value change events during assignment.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value) =>
            SetValueInternal(value);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Scrollbar.onValueChanged"/> when using
        /// <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <remarks>
        /// When <see cref="BindMode.OneWayToSource"/> is active, the current scrollbar value is also
        /// immediately forwarded to the ViewModel.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            CachedComponent.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(CachedComponent.value);
        }

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Scrollbar.onValueChanged"/> if active.
        /// </summary>
        protected override void OnUnbound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <summary>
        /// Applies <paramref name="value"/> to the scrollbar without reading the write back as user input.
        /// </summary>
        /// <remarks>
        /// The value is clamped to 0..1 before assignment; the reverse channel is raised only when the
        /// clamp changed it, not when the converter did.
        /// </remarks>
        protected void SetValueInternal(float value)
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
                // Without finally, an exception from the setter (e.g. a foreign onValueChanged listener) would leave the flag stuck off.
                _isNotifyValueChanged = true;
            }

            if (!Mathf.Approximately(clamped, converted)) OnValueChanged(clamped);
        }

        private void OnValueChanged(float value)
        {
            if (!_isNotifyValueChanged) return;
            _channel.Raise(GetConvertedBackValue(value));
        }

        /// <summary>
        /// Converts a value on its way back to the ViewModel.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        /// <returns>
        /// The value as the ViewModel expects it: undone by the converter when it offers
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>, and unchanged when it does not.
        /// </returns>
        /// <remarks>
        /// The raw value is returned, not the forward-converted one — applying the forward conversion
        /// again would write the View's presentation back into the ViewModel.
        /// </remarks>
        private float GetConvertedBackValue(float value) =>
            _converter is ITwoWayConverter<float, float> twoWay ? twoWay.ConvertBack(value) : value;
    }
}
