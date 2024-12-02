using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Visible Enum")]
    public sealed class ImageVisibleEnumMonoBinder : EnumMonoBinder<Image, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}