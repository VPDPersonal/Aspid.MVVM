// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Sliders
{
    public class SliderValueBinder : SliderBinderBase, IBinderNumber
    {
        public void SetValue(int value) =>
            CachedSlider.value = value;

        public void SetValue(long value) =>
            CachedSlider.value = value;

        public void SetValue(float value) =>
            CachedSlider.value = value;

        public void SetValue(double value) =>
            SetValue((float)value);
    }
}