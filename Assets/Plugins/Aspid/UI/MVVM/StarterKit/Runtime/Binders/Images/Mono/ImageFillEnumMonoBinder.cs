using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Fill Enum")]
    public sealed class ImageFillEnumMonoBinder : EnumMonoBinder<Image, float>
    {
        protected override void SetValue(float value) =>
            CachedComponent.fillAmount = value;
    }
}