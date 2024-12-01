using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Visible Enum")]
    public sealed class RawImageVisibleEnumMonoBinder : EnumMonoBinder<RawImage, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}