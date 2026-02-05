#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class RawImageTextureSwitcherBinder : SwitcherBinder<RawImage, Texture2D?>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private bool _disabledWhenNull = true;

        public RawImageTextureSwitcherBinder(
            RawImage target,
            Texture2D trueValue, 
            Texture2D falseValue,
            BindMode mode) 
            : this(target, trueValue, falseValue, disabledWhenNull: true, mode) { }
        
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
            Target.enabled = !_disabledWhenNull || value;
        }
    }
}