using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Image), "m_Sprite")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder - Sprite EnumGroup")]
    [AddComponentContextMenu(typeof(Image),"Add Image Binder/Image Binder - Sprite EnumGroup")]
    public sealed class ImageSpriteEnumGroupMonoBinder : EnumGroupMonoBinder<Image>
    {
        [Header("Parameters")]
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