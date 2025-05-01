#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class RawImageTextureSwitcherBinder : SwitcherBinder<RawImage, Texture2D?>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameter")]
        [SerializeField] private bool _disabledWhenNull = true;

        public RawImageTextureSwitcherBinder(
            RawImage target,
            Texture2D trueValue, 
            Texture2D falseValue,
            BindMode mode) 
            : this(target, trueValue, falseValue, true, mode) { }
        
        public RawImageTextureSwitcherBinder(
            RawImage target,
            Texture2D trueValue, 
            Texture2D falseValue, 
            bool disabledWhenNull = true,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
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