// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    public partial class ImageFillBinder : ImageBinderBase, IBinder<float>
    {
        [BinderLog]
        public void SetValue(float value) =>
            CachedImage.fillAmount = value;
    }
}