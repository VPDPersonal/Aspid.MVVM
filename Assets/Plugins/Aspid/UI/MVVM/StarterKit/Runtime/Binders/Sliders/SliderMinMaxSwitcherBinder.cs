#nullable enable
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public sealed class SliderMinMaxSwitcherBinder : SwitcherBinder<Vector2>
    {
        private readonly Slider _slider;
        private readonly SliderValueMode _mode;

        public SliderMinMaxSwitcherBinder(Slider slider, Vector2 trueValue, Vector2 falseValue, SliderValueMode mode = SliderValueMode.Range) 
            : base(trueValue, falseValue)
        {
            _mode = mode;
            _slider = slider;
        }

        protected override void SetValue(Vector2 value) =>
            _slider.SetMinMax(value, _mode);
    }
}