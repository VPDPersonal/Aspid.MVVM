using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Sprite")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Sprite EnumGroup")]
    public sealed class ImageSpriteEnumGroupMonoBinder : EnumGroupMonoBinder<Image>
    {
        [Header("Values")]
        [SerializeField] private Sprite _defaultValue;
        [SerializeField] private Sprite _selectedValue;
        
        [SerializeField] private bool _disabledWhenNull = true;

        protected override void SetDefaultValue(Image element) =>
            SetValue(element, _defaultValue);

        protected override void SetSelectedValue(Image element) =>
            SetValue(element, _selectedValue);
        
        private void SetValue(Image element, Sprite value) 
        {
            element.sprite = value;
            element.enabled = !_disabledWhenNull || value;
        }
    }
}