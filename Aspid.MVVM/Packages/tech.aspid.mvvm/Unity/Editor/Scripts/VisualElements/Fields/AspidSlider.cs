using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="VisualElement"/> combining a slider and numeric input.
    /// </summary>
    public sealed class AspidSlider : VisualElement
    {
        private const string StyleSheetPath = "Styles/Fields/aspid-slider"; 
        
        private readonly Slider _slider;
        private readonly FloatField _input;
        
        /// <summary>
        /// Gets or sets the value, notifying listeners when it changes.
        /// </summary>
        public float Value
        {
            get => _input.value;
            set => SetValue(value);
        }

        /// <summary>
        /// Gets or sets the slider label.
        /// </summary>
        public string Label
        {
            get => _slider.label;
            set => _slider.label = value;
        }
        
        /// <summary>
        /// Creates a slider with numeric input over a range.
        /// </summary>
        /// <param name="label">The slider label; <see langword="null"/> for none.</param>
        /// <param name="start">The lower bound.</param>
        /// <param name="end">The upper bound.</param>
        public AspidSlider(string label = null, float start = 0, float end = 1)
        {
            _slider = new Slider(label, start, end);
            _input = new FloatField(label: null);
            
            _slider.RegisterValueChangedCallback(e =>
                _input.SetValue(e.newValue));
            
            _input.RegisterValueChangedCallback(e =>
            {
                var value = Mathf.Clamp(e.newValue, start, end);
                if (!Mathf.Approximately(value, e.newValue)) _input.SetValue(value);
                else _slider.SetValueWithoutNotify(value);
            });
            
            this.AddChild(_slider)
                .AddChild(_input)
                .styleSheets.Add(styleSheet: Resources.Load<StyleSheet>(StyleSheetPath));
        }

        /// <summary>
        /// Sets the value and notifies listeners when it changes.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public AspidSlider SetValue(float value)
        {
            _input.SetValue(value);
            return this;
        }
        
        /// <summary>
        /// Sets the numeric input without notifying listeners or updating the slider.
        /// </summary>
        /// <param name="value">The value to display in the numeric input.</param>
        /// <returns>The element, for chaining.</returns>
        public AspidSlider SetValueWithoutNotify(float value)
        {
            _input.SetValueWithoutNotify(value);
            return this;
        }

        /// <summary>
        /// Subscribes to value changes.
        /// </summary>
        /// <param name="callback">The handler to attach.</param>
        /// <returns>The element, for chaining.</returns>
        public AspidSlider RegisterValueChangedCallback(EventCallback<ChangeEvent<float>> callback)
        { 
            _input.RegisterValueChangedCallback(callback);
            return this;
        }

        /// <summary>
        /// Unsubscribes from value changes.
        /// </summary>
        /// <param name="callback">The handler to detach.</param>
        /// <returns>The element, for chaining.</returns>
        public AspidSlider UnregisterValueChangedCallback(EventCallback<ChangeEvent<float>> callback)
        { 
            _input.UnregisterValueChangedCallback(callback);
            return this;
        }
    }
}