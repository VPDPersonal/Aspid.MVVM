using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Sprite")]
    public partial class ImageSpriteBinder : ImageBinderBase, IBinder<Sprite>
    {
        [BinderLog]
        public void SetValue(Sprite value) =>
            CachedImage.sprite = value;
    }
}