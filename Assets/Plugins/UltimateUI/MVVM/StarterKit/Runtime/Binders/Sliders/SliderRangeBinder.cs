using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Sliders
{
    public partial class SliderRangeBinder : SliderBinderBase, IBinder<Vector2>
    {
        [BinderLog]
        public void SetValue(Vector2 value)
        {
            CachedSlider.minValue = value.x;
            CachedSlider.maxValue = value.y;
        }
    }
}