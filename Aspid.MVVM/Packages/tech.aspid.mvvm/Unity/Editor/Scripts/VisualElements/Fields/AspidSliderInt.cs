using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="VisualElement"/> combining a slider and numeric input.
    /// </summary>
    public sealed class AspidSliderInt : VisualElement
    {
        private const string StyleSheetPath = "Styles/Fields/aspid-slider"; 
        
        private readonly SliderInt _slider;
        private readonly IntegerField _input;
        
        /// <summary>
        /// Gets or sets the value, notifying listeners when it changes.
        /// </summary>
        public int Value
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
        public AspidSliderInt(string label = null, int start = 0, int end = 1)
        {
            _slider = new SliderInt(label, start, end);
            _input = new IntegerField(label: string.Empty);
            
            _slider.RegisterValueChangedCallback(e =>
                _input.SetValue(e.newValue));
            
            _input.RegisterValueChangedCallback(e =>
            {
                var value = Mathf.Clamp(e.newValue, start, end);
                if (value != e.newValue) _input.SetValue(value);
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
        public AspidSliderInt SetValue(int value)
        {
            _input.SetValue(value);
            return this;
        }
        
        /// <summary>
        /// Sets the numeric input without notifying listeners or updating the slider.
        /// </summary>
        /// <param name="value">The value to display in the numeric input.</param>
        /// <returns>The element, for chaining.</returns>
        public AspidSliderInt SetValueWithoutNotify(int value)
        {
            _input.SetValueWithoutNotify(value);
            return this;
        }

        /// <summary>
        /// Subscribes to value changes.
        /// </summary>
        /// <param name="callback">The handler to attach.</param>
        /// <returns>The element, for chaining.</returns>
        public AspidSliderInt RegisterValueChangedCallback(EventCallback<ChangeEvent<int>> callback)
        { 
            _input.RegisterValueChangedCallback(callback);
            return this;
        }

        /// <summary>
        /// Unsubscribes from value changes.
        /// </summary>
        /// <param name="callback">The handler to detach.</param>
        /// <returns>The element, for chaining.</returns>
        public AspidSliderInt UnregisterValueChangedCallback(EventCallback<ChangeEvent<int>> callback)
        { 
            _input.UnregisterValueChangedCallback(callback);
            return this;
        }
    }
}