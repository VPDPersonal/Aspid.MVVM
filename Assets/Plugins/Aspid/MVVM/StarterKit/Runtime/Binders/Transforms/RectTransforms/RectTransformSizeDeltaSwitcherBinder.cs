#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RectTransformSizeDeltaSwitcherBinder : SwitcherBinder<RectTransform, Vector2>
    {
        [SerializeField] private SizeDeltaMode _mode;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Vector2, Vector2>? _converter;

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
            IConverter<Vector2, Vector2>? converter)
            : this(target, trueValue, falseValue, SizeDeltaMode.SizeDelta, converter) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SizeDeltaMode mode = SizeDeltaMode.SizeDelta,
            IConverter<Vector2, Vector2>? converter = null)
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