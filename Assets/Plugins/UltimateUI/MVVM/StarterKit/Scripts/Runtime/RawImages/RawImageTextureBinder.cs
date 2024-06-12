using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.RawImages
{
    public class RawImageTextureBinder : RawImageBinderBase, IBinder<Texture2D>
    {
        public void SetValue(Texture2D value) =>
            CachedImage.texture = value;
    }
}