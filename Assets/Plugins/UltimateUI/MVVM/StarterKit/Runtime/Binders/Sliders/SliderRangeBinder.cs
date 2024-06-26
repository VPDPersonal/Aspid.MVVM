using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Sliders
{
    public class SliderRangeBinder : SliderBinderBase, IBinder<Vector2>
    {
        public void SetValue(Vector2 value)
        {
            CachedSlider.minValue = value.x;
            CachedSlider.maxValue = value.y;
        }
    }
}