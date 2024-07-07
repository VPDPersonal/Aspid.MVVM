// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Sliders
{
    public partial class SliderValueBinder : SliderBinderBase, IBinderNumber
    {
        [BinderLog]
        public void SetValue(int value) =>
            CachedSlider.value = value;

        [BinderLog]
        public void SetValue(long value) =>
            CachedSlider.value = value;

        [BinderLog]
        public void SetValue(float value) =>
            CachedSlider.value = value;

        public void SetValue(double value) =>
            SetValue((float)value);
    }
}