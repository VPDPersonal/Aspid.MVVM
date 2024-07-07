using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    public partial class ImageSpriteBinder : ImageBinderBase, IBinder<Sprite>
    {
        [BinderLog]
        public void SetValue(Sprite value) =>
            CachedImage.sprite = value;
    }
}