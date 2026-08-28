using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{Slider}"/> implementing <see cref="IFloatBinder"/> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;float&gt;</see> that binds <see cref="BaseSlider{T}.value"/>.
    /// </summary>
    /// <remarks>
    /// A write by the binder is not read back as a drag; the guard is released in a <see langword="finally"/> so an
    /// exception from another listener cannot leave the reverse channel stuck.
    /// </remarks>
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Slider Value")]
    [AddBinderContextMenuByType(typeof(float))]
    public sealed partial class ElementSliderValueMonoBinder : VisualElementMonoBinder<Slider>, 
        IFloatBinder,
        IReverseBinder<float>
    {
        /// <inheritdoc/>
        public event Action<float> ValueChanged;

        private bool _isNotifying = true;
        
        /// <summary>
        /// Sets the slider without reading the write back as a drag.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            var element = Element;
            if (element is null) return;

            if (!this.RequireFinite(value)) return;

            _isNotifying = false;

            try
            {
                element.value = value;
            }
            finally
            {
                _isNotifying = true;
            }
        }

        /// <summary>
        /// Called when the binder is bound. Listens to the slider when the mode carries values back, and sends the
        /// current value once in <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            var element = Element;
            if (element is null) return;

            element.RegisterValueChangedCallback(OnSliderChanged);
            if (Mode is BindMode.OneWayToSource) ValueChanged?.Invoke(element.value);
        }

        /// <summary>
        /// Called when the binder is unbound. Stops listening to the slider.
        /// </summary>
        protected override void OnUnbound()
        {
            if (Element is not null) Element.UnregisterValueChangedCallback(OnSliderChanged);
            base.OnUnbound();
        }

        private void OnSliderChanged(ChangeEvent<float> changed)
        {
            if (!_isNotifying) return;
            ValueChanged?.Invoke(changed.newValue);
        }
    }
}
