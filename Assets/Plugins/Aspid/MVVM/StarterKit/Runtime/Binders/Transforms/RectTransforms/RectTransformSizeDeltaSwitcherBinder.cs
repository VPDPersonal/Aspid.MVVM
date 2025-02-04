#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterVector2;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RectTransformSizeDeltaSwitcherBinder : SwitcherBinder<RectTransform, Vector2>
    {
        [SerializeField] private SizeDeltaMode _sizeMode;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, SizeDeltaMode.SizeDelta, null, mode) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SizeDeltaMode sizeMode,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, sizeMode, null, mode) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, SizeDeltaMode.SizeDelta, converter, mode) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SizeDeltaMode sizeMode = SizeDeltaMode.SizeDelta,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            _sizeMode = sizeMode;
            _converter = converter; 
        }

        protected override void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetSizeDelta(value, _sizeMode);
        }
    }
}