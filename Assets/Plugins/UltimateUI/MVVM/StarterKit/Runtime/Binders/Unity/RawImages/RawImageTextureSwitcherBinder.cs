using UnityEngine;
using UnityEngine.UI;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.RawImages
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Texture Switcher")]
    public sealed class RawImageTextureSwitcherBinder : SwitcherMonoBinder<RawImage, Texture2D>
    {
        protected override void SetValue(Texture2D value) =>
            CachedComponent.texture = value;
    }
}