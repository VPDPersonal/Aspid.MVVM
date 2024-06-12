using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.RawImages
{
    public class RawImageTextureSwitcherBinder : RawImageBinderBase, IBinder<Texture2D>
    {
        [Header("Parameters")]
        [SerializeField] private Texture2D _trueTexture;
        [SerializeField] private Texture2D _falseTexture;

        public void SetValue(Texture2D value) =>
            CachedImage.texture = value;
    }
}