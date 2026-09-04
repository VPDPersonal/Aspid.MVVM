using System;
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
        IReverseBinder<float>
    {
        private bool _isNotifyValueChanged = true;

        /// <inheritdoc/>
        public event Action<float> ValueChanged;

        /// <summary>
        /// Sets the value without reporting the write back to the ViewModel.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            var element = Element;
            if (element is null) return;
            if (!this.RequireFinite(value)) return;

            _isNotifyValueChanged = false;

            try
            {
                element.value = value;
            }
            finally
            {
                // Keeps the reverse channel alive when another listener throws.
                _isNotifyValueChanged = true;
            }
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            var element = Element;
            if (element is null) return;

            element.RegisterValueChangedCallback(OnSliderChanged);
            if (Mode is BindMode.OneWayToSource) ValueChanged?.Invoke(element.value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Element is not null) Element.UnregisterValueChangedCallback(OnSliderChanged);
            base.OnUnbound();
        }

        private void OnSliderChanged(ChangeEvent<float> changed)
        {
            if (_isNotifyValueChanged)
                ValueChanged?.Invoke(changed.newValue);
        }
    }
}
