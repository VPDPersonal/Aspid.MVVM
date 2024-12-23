#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ImageSpriteSwitcherBinder : SwitcherBinder<Image, Sprite?>
    {
        [SerializeField] private bool _disabledWhenNull;

        public ImageSpriteSwitcherBinder(
            Image target,
            Sprite trueValue, 
            Sprite falseValue, 
            bool disabledWhenNull = true) 
            : base(target, trueValue, falseValue)
        {
            _disabledWhenNull = disabledWhenNull;
        }

        protected override void SetValue(Sprite? value)
        {
            Target.sprite = value;
            if (_disabledWhenNull) Target.enabled = value is not null;
        }
    }
}