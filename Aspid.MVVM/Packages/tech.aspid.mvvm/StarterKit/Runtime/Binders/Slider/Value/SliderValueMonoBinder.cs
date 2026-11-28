#if ASPID_MVVM_HAS_UGUI
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that binds <see cref="Slider.value"/>, also from other numbers,
    /// and reports user changes back as numbers.
    /// </summary>
    [BindModeOverride(IsAll = true)]
    [AddBinderContextMenu(
        type: typeof(Slider),
        serializePropertyNames: "m_Value")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value")]
    public partial class SliderValueMonoBinder : ComponentMonoBinder<Slider>, IFloatBinder, INumberReverseBinder
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Always;

        [Tooltip("Optional converter applied to the value; reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<float, float> _converter;

        private uint _generation;
        private bool _isSuppressingEcho;
        private bool _hasPendingNormalization;
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
        /// Sets <see cref="Slider.value"/> without reporting the write back to the ViewModel.
        /// </summary>
        /// <remarks>
        /// TwoWay reports the applied value back when clamping or whole-number rounding changes it;
        /// the initial correction waits until binding completes.
        /// </remarks>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            var generation = _generation;
            var slider = CachedComponent;
            var converted = _converter?.Convert(value) ?? value;
            var clamped = this.SafeClamp(converted, slider.minValue, slider.maxValue);

            if (_notifyMode.ShouldNotify(IsBound))
            {
                _isSuppressingEcho = true;

                try
                {
                    slider.value = clamped;
                }
                finally
                {
                    _isSuppressingEcho = false;
                }
            }
            else
            {
                slider.SetValueWithoutNotify(clamped);
            }

            if (generation != _generation) return;

            var applied = slider.value;
            var needsNormalization = !Mathf.Approximately(applied, converted);

            if (!IsBound)
                _hasPendingNormalization = Mode is BindMode.TwoWay && needsNormalization;

            if (Mode is BindMode.TwoWay && needsNormalization)
                OnValueChanged(applied);
        }

        /// <inheritdoc/>
        protected override void OnBinding()
        {
            base.OnBinding();

            unchecked { _generation++; }
            _hasPendingNormalization = false;
        }

        /// <inheritdoc/>
        protected override void OnAttach()
        {
            if (!Mode.IncludesReverse()) return;

            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            var normalize = _hasPendingNormalization;
            _hasPendingNormalization = false;

            if (Mode is BindMode.OneWayToSource || Mode is BindMode.TwoWay && normalize)
                OnValueChanged(CachedComponent.value);
        }

        /// <inheritdoc/>
        protected override void OnDetach()
        {
            unchecked { _generation++; }
            _hasPendingNormalization = false;
            if (Mode.IncludesReverse())
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
#endif
