using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Texture Enum")]
    public sealed class RawImageTextureEnumMonoBinder : EnumMonoBinder<RawImage, Texture2D>
    {
        protected override void SetValue(Texture2D value) =>
            CachedComponent.texture = value;
    }
}