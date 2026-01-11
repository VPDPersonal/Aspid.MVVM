using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder – Texture EnumGroup")]
    public sealed class RawImageTextureEnumGroupMonoBinder : EnumGroupMonoBinder<RawImage>
    {
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
            element.enabled = !_disabledWhenNull || value;
        }
    }
}