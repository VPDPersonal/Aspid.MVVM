#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ImageSpriteSwitcherBinder : SwitcherBinder<Sprite?>
    {
        [SerializeField] private bool _disabledWhenNull;
        
        [Header("Component")]
        [SerializeField] private Image _image;

        public ImageSpriteSwitcherBinder(Image image, Sprite trueValue, Sprite falseValue, bool disabledWhenNull = true) 
            : base(trueValue, falseValue)
        {
            _disabledWhenNull = disabledWhenNull;
            _image = image ?? throw new ArgumentNullException();
        }

        protected override void SetValue(Sprite? value)
        {
            _image.sprite = value;
            if (_disabledWhenNull) _image.enabled = value is not null;
        }
    }
}