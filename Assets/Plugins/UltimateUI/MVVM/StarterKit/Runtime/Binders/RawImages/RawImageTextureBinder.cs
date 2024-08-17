using UnityEngine;
using UltimateUI.MVVM.Unity.Generation;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.RawImages
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Texture")]
    public partial class RawImageTextureBinder : RawImageBinderBase, IBinder<Texture2D>
    {
        [BinderLog]
        public void SetValue(Texture2D value) =>
            CachedImage.texture = value;
    }
}