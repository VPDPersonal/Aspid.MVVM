// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    public class ImageFillBinder : ImageBinderBase, IBinder<float>
    {
        public void SetValue(float value) =>
            CachedImage.fillAmount = value;
    }
}