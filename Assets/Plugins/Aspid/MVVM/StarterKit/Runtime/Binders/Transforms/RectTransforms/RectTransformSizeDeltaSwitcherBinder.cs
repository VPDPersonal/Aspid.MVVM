#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterVector2
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RectTransformSizeDeltaSwitcherBinder : SwitcherBinder<RectTransform, Vector2>
    {
        [SerializeField] private SizeDeltaMode _mode;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            Func<Vector2, Vector2> converter)
            : this(target, trueValue, falseValue, SizeDeltaMode.SizeDelta, converter.ToConvert()) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SizeDeltaMode mode,
            Func<Vector2, Vector2> converter)
            : this(target, trueValue, falseValue, mode, converter.ToConvert()) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            Converter? converter)
            : this(target, trueValue, falseValue, SizeDeltaMode.SizeDelta, converter) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SizeDeltaMode mode = SizeDeltaMode.SizeDelta,
            Converter? converter = null)
            : base(target, trueValue, falseValue)
        {
            _mode = mode;
            _converter = converter; 
        }

        protected override void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetSizeDelta(value, _mode);
        }
    }
}