using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    public class ImageSpriteBinder : ImageBinderBase, IBinder<Sprite>
    {
        public void SetValue(Sprite value) =>
            CachedImage.sprite = value;
    }
}