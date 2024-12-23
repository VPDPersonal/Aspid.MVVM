#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RawImageTextureSwitcherBinder : SwitcherBinder<RawImage, Texture2D?>
    {
        [Header("Parameter")]
        [SerializeField] private bool _disabledWhenNull;

        public RawImageTextureSwitcherBinder(
            RawImage target,
            Texture2D trueValue, 
            Texture2D falseValue, 
            bool disabledWhenNull) 
            : base(target, trueValue, falseValue)
        {
            _disabledWhenNull = disabledWhenNull; 
        }

        protected override void SetValue(Texture2D? value)
        {
            Target.texture = value;
            if (_disabledWhenNull) Target.enabled = value is not null;
        }
    }
}