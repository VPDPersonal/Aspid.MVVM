using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{Slider}"/> implementing <see cref="INumberBinder"/> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;float&gt;</see> that binds <see cref="BaseSlider{T}.value"/>.
    /// </summary>
    /// <remarks>
    /// Two-way, like its uGUI counterpart: the ViewModel sets the slider and the user's drag reaches the ViewModel.
    /// <para/>
    /// A write by the binder is not read back as a drag. UI Toolkit raises the same change event for both, so the guard is
    /// the only thing that keeps the ViewModel from receiving its own value back — and it is released in a
    /// <see langword="finally"/>, because an exception from another listener would otherwise leave the reverse channel
    /// dead for good.
    /// </remarks>
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Slider Value")]
    [AddBinderContextMenuByType(typeof(float))]
    public sealed partial class ElementSliderValueMonoBinder : VisualElementMonoBinder<Slider>, INumberBinder, IReverseBinder<float>
    {
        /// <inheritdoc/>
        public event Action<float> ValueChanged;

        private bool _isNotifying = true;

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets the slider.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        [BinderLog]
        public void SetValue(long value) => SetValue((float)value);

        /// <inheritdoc cref="SetValue(int)"/>
        /// <remarks>
        /// Narrowed to <see langword="float"/> — precision may be lost.
        /// </remarks>
        [BinderLog]
        public void SetValue(double value) => SetValue((float)value);

        /// <summary>
        /// Sets the slider without reading the write back as a drag.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            var element = Element;
            if (element is null) return;

            if (!BinderMath.IsFinite(value)) return;

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
