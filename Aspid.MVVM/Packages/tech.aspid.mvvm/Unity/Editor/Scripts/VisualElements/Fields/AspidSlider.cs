using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// A slider paired with a number box, both editing the same value.
    /// </summary>
    /// <remarks>
    /// The pair exists because a bare slider cannot be typed into and a bare field cannot be dragged. Each half
    /// updates the other without raising a change of its own, so a drag reports one change and not two.
    /// </remarks>
    public sealed class AspidSlider : VisualElement
    {
        private const string StyleSheetPath = "Styles/Fields/aspid-slider"; 
        
        private readonly Slider _slider;
        private readonly FloatField _input;
        
        /// <summary>
        /// Gets or sets the value, notifying listeners.
        /// </summary>
        public float Value
        {
            get => _input.value;
            set => SetValue(value);
        }

        /// <summary>
        /// Gets or sets the text shown beside the slider.
        /// </summary>
        public string Label
        {
            get => _slider.label;
            set => _slider.label = value;
        }
        
        /// <summary>
        /// Creates a slider over the given range.
        /// </summary>
        /// <param name="label">The text shown beside the slider, or <see langword="null"/> for none.</param>
        /// <param name="start">The low end of the range.</param>
        /// <param name="end">The high end of the range.</param>
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
        /// Sets the value and notifies listeners.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>This field, so calls can be chained.</returns>
        public AspidSlider SetValue(float value)
        {
            _input.SetValue(value);
            return this;
        }
        
        /// <summary>
        /// Sets the value without notifying listeners.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>This field, so calls can be chained.</returns>
        /// <remarks>
        /// For writing a value the field itself did not cause — reflecting the serialized state, for instance,
        /// where a notification would be read back as an edit.
        /// </remarks>
        public AspidSlider SetValueWithoutNotify(float value)
        {
            _input.SetValueWithoutNotify(value);
            return this;
        }

        /// <summary>
        /// Subscribes to value changes.
        /// </summary>
        /// <param name="callback">The handler to call when the value changes.</param>
        /// <returns>This field, so calls can be chained.</returns>
        public AspidSlider RegisterValueChangedCallback(EventCallback<ChangeEvent<float>> callback)
        { 
            _input.RegisterValueChangedCallback(callback);
            return this;
        }

        /// <summary>
        /// Unsubscribes from value changes.
        /// </summary>
        /// <param name="callback">The handler to detach.</param>
        /// <returns>This field, so calls can be chained.</returns>
        public AspidSlider UnregisterValueChangedCallback(EventCallback<ChangeEvent<float>> callback)
        { 
            _input.UnregisterValueChangedCallback(callback);
            return this;
        }
    }
}