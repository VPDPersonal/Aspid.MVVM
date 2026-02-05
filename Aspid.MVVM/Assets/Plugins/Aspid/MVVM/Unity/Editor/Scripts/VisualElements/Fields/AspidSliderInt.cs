using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class AspidSliderInt : VisualElement
    {
        private const string StyleSheetPath = "Styles/Fields/aspid-slider"; 
        
        private readonly SliderInt _slider;
        private readonly IntegerField _input;
        
        public int Value
        {
            get => _input.value;
            set => SetValue(value);
        }

        public string Label
        {
            get => _slider.label;
            set => _slider.label = value;
        }
        
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
        
        public AspidSliderInt SetValue(int value)
        {
            _input.SetValue(value);
            return this;
        }
        
        public AspidSliderInt SetValueWithoutNotify(int value)
        {
            _input.SetValueWithoutNotify(value);
            return this;
        }

        public AspidSliderInt RegisterValueChangedCallback(EventCallback<ChangeEvent<int>> callback)
        { 
            _input.RegisterValueChangedCallback(callback);
            return this;
        }

        public AspidSliderInt UnregisterValueChangedCallback(EventCallback<ChangeEvent<int>> callback)
        { 
            _input.UnregisterValueChangedCallback(callback);
            return this;
        }
    }
}