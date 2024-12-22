#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RectTransformSizeDeltaSwitcherBinder : SwitcherBinder<Vector2>
    {
        [SerializeField] private SizeDeltaMode _mode;
        
        [Header("Component")]
        [SerializeField] private RectTransform _transform;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Vector2, Vector2>? _converter;

        public RectTransformSizeDeltaSwitcherBinder(
            Vector2 trueValue, 
            Vector2 falseValue,
            RectTransform transform, 
            Func<Vector2, Vector2> converter)
            : this(trueValue, falseValue, transform, SizeDeltaMode.SizeDelta, converter.ToConvert()) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            Vector2 trueValue, 
            Vector2 falseValue,
            RectTransform transform, 
            SizeDeltaMode mode,
            Func<Vector2, Vector2> converter)
            : this(trueValue, falseValue, transform, mode, converter.ToConvert()) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            Vector2 trueValue, 
            Vector2 falseValue,
            RectTransform transform, 
            IConverter<Vector2, Vector2>? converter)
            : this(trueValue, falseValue, transform, SizeDeltaMode.SizeDelta, converter) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            Vector2 trueValue, 
            Vector2 falseValue,
            RectTransform transform, 
            SizeDeltaMode mode = SizeDeltaMode.SizeDelta,
            IConverter<Vector2, Vector2>? converter = null)
            : base(trueValue, falseValue)
        {
            _mode = mode;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        protected override void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            _transform.SetSizeDelta(value, _mode);
        }
    }
}