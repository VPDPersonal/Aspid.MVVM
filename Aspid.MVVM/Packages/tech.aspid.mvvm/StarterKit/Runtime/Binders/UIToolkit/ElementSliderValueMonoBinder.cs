#if ASPID_MVVM_HAS_UIELEMENTS
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{TElement}"/> that binds <see cref="BaseSlider{T}.value"/> of a
    /// <see cref="Slider"/> and reports user changes back.
    /// </summary>
    /// <remarks>
    /// Writes raise the change event for other listeners; only the binder's own echo is suppressed. A non-finite
    /// value is refused.
    /// </remarks>
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Slider Value")]
    [AddBinderContextMenuByType(typeof(float))]
    public sealed partial class ElementSliderValueMonoBinder : VisualElementMonoBinder<Slider>,
        IFloatBinder,
        INumberReverseBinder
    {
        private float _lastValue;
        private bool _hasValue;

        private bool _isSuppressingEcho;
        private NumberReverseChannel _channel;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Sets the value without reporting the write back to the ViewModel.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            if (!this.RequireFinite(value)) return;
            _lastValue = value;
            _hasValue = true;
            var element = ResolveForWrite();
            if (element is null) return;

            _isSuppressingEcho = true;

            try
            {
                element.value = value;
            }
            finally
            {
                // Keeps the reverse channel alive when another listener throws.
                _isSuppressingEcho = false;
            }
        }

        /// <inheritdoc/>
        protected override void OnElementAttached(Slider element)
        {
            if (!Mode.IncludesReverse()) return;
            element.UnregisterValueChangedCallback(OnSliderChanged);
            element.RegisterValueChangedCallback(OnSliderChanged);
            if (IsBound && Mode is BindMode.OneWayToSource) OnBound();
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;
            var element = Element;
            if (element is not null)
                _channel.Raise(element.value);
        }

        /// <inheritdoc/>
        protected override void OnElementDetached(Slider element) =>
            element.UnregisterValueChangedCallback(OnSliderChanged);

        /// <inheritdoc/>
        protected override void ClearValue()
        {
            _hasValue = false;
            _lastValue = default;
        }

        /// <inheritdoc/>
        protected override void RestoreElement()
        {
            if (_hasValue) SetValue(_lastValue);
        }

        private void OnSliderChanged(ChangeEvent<float> changed)
        {
            if (!_isSuppressingEcho)
                _channel.Raise(changed.newValue);
        }
    }
}
#endif
