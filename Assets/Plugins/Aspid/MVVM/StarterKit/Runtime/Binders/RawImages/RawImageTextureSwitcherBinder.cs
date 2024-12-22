#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RawImageTextureSwitcherBinder : SwitcherBinder<Texture2D?>
    {
        [Header("Component")]
        [SerializeField] private RawImage _image;
        
        [Header("Parameter")]
        [SerializeField] private bool _disabledWhenNull;

        public RawImageTextureSwitcherBinder(
            Texture2D trueValue, 
            Texture2D falseValue, 
            RawImage image,
            bool disabledWhenNull) 
            : base(trueValue, falseValue)
        {
            _disabledWhenNull = disabledWhenNull;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        protected override void SetValue(Texture2D? value)
        {
            _image.texture = value;
            if (_disabledWhenNull) _image.enabled = value is not null;
        }
    }
}