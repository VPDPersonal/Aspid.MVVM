using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder - Texture EnumGroup")]
    public sealed class RawImageTextureEnumGroupMonoBinder : EnumGroupMonoBinder<RawImage>
    {
        [Header("Parameters")]
        [SerializeField] private Texture2D _defaultValue;
        [SerializeField] private Texture2D _selectedValue;
        
        [SerializeField] private bool _disabledWhenNull = true;

        protected override void SetDefaultValue(RawImage element) =>
            SetValue(element, _defaultValue);

        protected override void SetSelectedValue(RawImage element) =>
            SetValue(element, _selectedValue);
        
        private void SetValue(RawImage element, Texture2D value) 
        {
            element.texture = value;
            if (_disabledWhenNull) element.enabled = value is not null;
        }
    }
}