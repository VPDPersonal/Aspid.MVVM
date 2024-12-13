using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Image/Image Binder - Fill Enum Group")]
    public sealed class ImageFillEnumGroupMonoBinder : EnumGroupMonoBinder<Image, float>
    {
        protected override void SetValue(Image component, float value) =>
            component.fillAmount = value;
    }
}