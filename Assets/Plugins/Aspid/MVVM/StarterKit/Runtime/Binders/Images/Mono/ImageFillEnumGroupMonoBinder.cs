using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Image/Image Binder - Fill Enum Group")]
    public sealed class ImageFillEnumGroupMonoBinder : EnumGroupMonoBinder<Image>
    {
        [Header("Parameters")]
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _selectedValue;
        
        protected override void SetDefaultValue(Image component) =>
            component.fillAmount = _defaultValue;

        protected override void SetSelectedValue(Image component) =>
            component.fillAmount = _selectedValue;
    }
}