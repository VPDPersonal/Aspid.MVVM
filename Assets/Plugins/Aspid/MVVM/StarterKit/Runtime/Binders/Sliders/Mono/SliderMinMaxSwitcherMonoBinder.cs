using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Slider/Slider Binder - Min Max Switcher")]
    public sealed class SliderMinMaxSwitcherMonoBinder : SwitcherMonoBinder<Slider, Vector2>
    {
        [SerializeField] private SliderValueMode _mode = SliderValueMode.Range;
        
        protected override void SetValue(Vector2 value) =>
            CachedComponent.SetMinMax(value, _mode);
    }
}