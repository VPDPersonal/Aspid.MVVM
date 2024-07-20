using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.RawImages
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Texture Switcher")]
    public partial class RawImageTextureSwitcherBinder : RawImageBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Texture2D _trueTexture;
        [SerializeField] private Texture2D _falseTexture;

        protected Texture2D TrueTexture => _trueTexture;
        
        protected Texture2D FalseTexture => _falseTexture;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedImage.texture = GetTexture2D(value);

        protected Texture2D GetTexture2D(bool value) =>
            value ? TrueTexture : FalseTexture;
    }
}