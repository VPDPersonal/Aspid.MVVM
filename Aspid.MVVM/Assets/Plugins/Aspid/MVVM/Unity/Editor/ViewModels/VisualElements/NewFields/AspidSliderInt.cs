using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class AspidSliderInt : VisualElement
    {
        private readonly IntegerField _input;
        
        internal AspidSliderInt(string label = null, int start = 0, int end = 1)
        {
            var slider = new SliderInt(label, start, end)
                .SetFlexGrow(1f);

            _input = new IntegerField(label: string.Empty)
                .SetSize(width: 60);
            
            slider.RegisterValueChangedCallback(e =>
                _input.SetValueWithoutNotify(e.newValue));
            
            _input.RegisterValueChangedCallback(e =>
            {
                var value = Mathf.Clamp(e.newValue, start, end);
                if (value != e.newValue) _input.SetValueWithoutNotify(value);
                slider.SetValueWithoutNotify(value);
            });
            
            this.SetFlexDirection(FlexDirection.Row)
                .AddChild(slider.SetMargin(right: 5))
                .AddChild(_input);
        }

        internal AspidSliderInt SetValue(int value)
        {
            _input.SetValue(value);
            return this;
        }
        
        internal void SetValueWithoutNotify(int value) =>
            _input.SetValueWithoutNotify(value);
        
        internal void RegisterValueChangedCallback(EventCallback<ChangeEvent<int>> callback) =>
            _input.RegisterValueChangedCallback(callback);
    }
}
