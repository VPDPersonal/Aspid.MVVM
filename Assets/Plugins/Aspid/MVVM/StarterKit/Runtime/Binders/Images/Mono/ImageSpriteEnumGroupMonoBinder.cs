using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Image/Image Binder - Sprite Enum Group")]
    public sealed class ImageSpriteEnumGroupMonoBinder : EnumGroupMonoBinder<Image>
    {
        [Header("Parameters")]
        [SerializeField] private Sprite _defaultValue;
        [SerializeField] private Sprite _selectedValue;
        [SerializeField] private bool _disabledWhenNull = true;

        protected override void SetDefaultValue(Image component) =>
            SetValue(component, _defaultValue);

        protected override void SetSelectedValue(Image component) =>
            SetValue(component, _selectedValue);
        
        private void SetValue(Image image, Sprite value) 
        {
            image.sprite = value;
            if (_disabledWhenNull) image.enabled = value is not null;
        }
    }
}