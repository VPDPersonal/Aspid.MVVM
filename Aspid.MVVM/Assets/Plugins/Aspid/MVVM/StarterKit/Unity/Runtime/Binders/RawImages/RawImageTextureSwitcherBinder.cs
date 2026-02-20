#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Texture?, UnityEngine.Texture?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterTexture;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class RawImageTextureSwitcherBinder : SwitcherBinder<RawImage, Texture?, Converter>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private bool _disabledWhenNull = true;

        public RawImageTextureSwitcherBinder(
            RawImage target,
            Texture trueValue, 
            Texture falseValue,
            BindMode mode) 
            : this(target, trueValue, falseValue, disabledWhenNull: true, null, mode) { }
        
        public RawImageTextureSwitcherBinder(
            RawImage target,
            Texture trueValue, 
            Texture falseValue, 
            bool disabledWhenNull = true,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode)
        {
            _disabledWhenNull = disabledWhenNull; 
        }

        protected override void SetValue(Texture? value)
        {
            Target.texture = value;
            Target.enabled = !_disabledWhenNull || value;
        }
    }
}