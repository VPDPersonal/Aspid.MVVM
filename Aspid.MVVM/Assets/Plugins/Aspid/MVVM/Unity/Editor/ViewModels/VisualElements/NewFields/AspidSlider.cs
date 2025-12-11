using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class AspidSlider : VisualElement
    {
        private readonly FloatField _input;
        
        internal AspidSlider(string label = null, float start = 0, float end = 1)
        {
            var slider = new Slider(label, start, end)
                .SetFlexGrow(1f);

            _input = new FloatField(label: string.Empty)
                .SetSize(width: 60);
            
            slider.RegisterValueChangedCallback(e =>
                _input.SetValueWithoutNotify(e.newValue));
            
            _input.RegisterValueChangedCallback(e =>
            {
                var value = Mathf.Clamp(e.newValue, start, end);
                if (!Mathf.Approximately(value, e.newValue)) _input.SetValueWithoutNotify(value);
                slider.SetValueWithoutNotify(value);
            });
            
            this.SetFlexDirection(FlexDirection.Row)
                .AddChild(slider.SetMargin(right: 5))
                .AddChild(_input);
        }

        internal AspidSlider SetValue(float value)
        {
            _input.SetValue(value);
            return this;
        }
        
        internal void SetValueWithoutNotify(float value) =>
            _input.SetValueWithoutNotify(value);
        
        internal void RegisterValueChangedCallback(EventCallback<ChangeEvent<float>> callback) =>
            _input.RegisterValueChangedCallback(callback);
    }
}
