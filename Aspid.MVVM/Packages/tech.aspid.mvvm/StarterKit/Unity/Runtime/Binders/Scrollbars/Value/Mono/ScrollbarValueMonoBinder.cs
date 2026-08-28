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
    /// Supports <see cref="BindMode.TwoWay"/> and <see cref="BindMode.OneWayToSource"/>: when
    /// <see cref="Scrollbar.onValueChanged"/> fires, the current value is forwarded to the ViewModel.
    /// When <see cref="BindMode.OneWayToSource"/> is active, the current value is also immediately
    /// forwarded when binding is established.
    /// <para/>
    /// A scrollbar has no configurable range: its value is always normalised to 0..1, so the incoming
    /// value is clamped to that range rather than to inspector-set bounds.
    /// </remarks>
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
        /// When <see cref="BindMode.OneWayToSource"/> is active, the current scrollbar value is also immediately
        /// forwarded to the ViewModel to synchronize the source with the current view state.
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
        /// The value is clamped to 0..1 first, and the reverse channel is told whenever the clamp changed it —
        /// otherwise the ViewModel would keep a value the scrollbar never held. A converter's own effect is not
        /// reported back: only the difference the clamp made is.
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
            _channel.Raise(value);
        }
    }
}
