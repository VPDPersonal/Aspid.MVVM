using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Texture Switcher")]
    public sealed class RawImageTextureSwitcherMonoBinder : SwitcherMonoBinder<RawImage, Texture2D>
    {
        protected override void SetValue(Texture2D value) =>
            CachedComponent.texture = value;
    }
}