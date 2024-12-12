using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Image/Image Binder - Visible Enum Group")]
    public sealed class ImageVisibleEnumGroupMonoBinder : EnumGroupMonoBinder<Image>
    {
        [Header("Parameter")]
        [SerializeField] private bool _isInvert;
        
        protected override void SetDefaultValue(Image component) =>
            component.enabled = _isInvert;

        protected override void SetSelectedValue(Image component) =>
            component.enabled = !_isInvert;
    }
}