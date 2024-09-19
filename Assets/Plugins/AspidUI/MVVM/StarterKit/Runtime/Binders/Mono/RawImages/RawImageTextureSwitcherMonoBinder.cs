using UnityEngine;
using UnityEngine.UI;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.RawImages
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Texture Switcher")]
    public sealed class RawImageTextureSwitcherMonoBinder : SwitcherMonoBinder<RawImage, Texture2D>
    {
        protected override void SetValue(Texture2D value) =>
            CachedComponent.texture = value;
    }
}