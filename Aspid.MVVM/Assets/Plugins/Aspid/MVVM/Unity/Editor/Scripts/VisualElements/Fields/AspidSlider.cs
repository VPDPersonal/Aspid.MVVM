using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class AspidSlider : VisualElement
    {
        private const string StyleSheetPath = "Styles/Fields/aspid-slider"; 
        
        private readonly Slider _slider;
        private readonly FloatField _input;
        
        public float Value
        {
            get => _input.value;
            set => SetValue(value);
        }

        public string Label
        {
            get => _slider.label;
            set => _slider.label = value;
        }
        
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

        public AspidSlider SetValue(float value)
        {
            _input.SetValue(value);
            return this;
        }
        
        public AspidSlider SetValueWithoutNotify(float value)
        {
            _input.SetValueWithoutNotify(value);
            return this;
        }

        public AspidSlider RegisterValueChangedCallback(EventCallback<ChangeEvent<float>> callback)
        { 
            _input.RegisterValueChangedCallback(callback);
            return this;
        }

        public AspidSlider UnregisterValueChangedCallback(EventCallback<ChangeEvent<float>> callback)
        { 
            _input.UnregisterValueChangedCallback(callback);
            return this;
        }
    }
}