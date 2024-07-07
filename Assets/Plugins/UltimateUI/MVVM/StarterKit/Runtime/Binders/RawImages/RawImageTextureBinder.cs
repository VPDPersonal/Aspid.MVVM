using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.RawImages
{
    public partial class RawImageTextureBinder : RawImageBinderBase, IBinder<Texture2D>
    {
        [BinderLog]
        public void SetValue(Texture2D value) =>
            CachedImage.texture = value;
    }
}