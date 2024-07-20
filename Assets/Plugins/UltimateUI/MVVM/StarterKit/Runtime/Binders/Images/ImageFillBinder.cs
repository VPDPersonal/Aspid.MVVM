using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Fill")]
    public partial class ImageFillBinder : ImageBinderBase, IBinder<float>
    {
        [BinderLog]
        public void SetValue(float value) =>
            CachedImage.fillAmount = value;
    }
}