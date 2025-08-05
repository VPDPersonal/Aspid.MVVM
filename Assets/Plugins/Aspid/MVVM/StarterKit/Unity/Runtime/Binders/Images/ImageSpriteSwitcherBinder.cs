#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class ImageSpriteSwitcherBinder : SwitcherBinder<Image, Sprite?>
    {
        [Header("Parameter")]
        [SerializeField] private bool _disabledWhenNull;

        public ImageSpriteSwitcherBinder(
            Image target,
            Sprite trueValue, 
            Sprite falseValue,
            BindMode mode) 
            : this(target, trueValue, falseValue, true, mode) { }
        
        public ImageSpriteSwitcherBinder(
            Image target,
            Sprite trueValue, 
            Sprite falseValue, 
            bool disabledWhenNull = true,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
        {
            _disabledWhenNull = disabledWhenNull;
        }

        protected override void SetValue(Sprite? value)
        {
            Target.sprite = value;
            Target.enabled = !_disabledWhenNull || value;
        }
    }
}