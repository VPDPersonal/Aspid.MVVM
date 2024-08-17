using UnityEngine;
using UltimateUI.MVVM.Unity.Generation;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Sliders
{
    [AddComponentMenu("UI/Binders/Slider/Slider Binder - Range")]
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