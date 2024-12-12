using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Image/Image Binder - Visible Enum")]
    public sealed class ImageVisibleEnumMonoBinder : EnumMonoBinder<Image, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}