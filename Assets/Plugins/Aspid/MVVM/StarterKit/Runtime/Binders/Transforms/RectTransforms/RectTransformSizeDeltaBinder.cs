#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class RectTransformSizeDeltaBinder : TargetBinder<RectTransform>, IBinder<Vector2>, INumberBinder
    {
        [Header("Parameter")]
        [SerializeField] private SizeDeltaMode _mode;

#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Vector2, Vector2>? _converter;

        public RectTransformSizeDeltaBinder(RectTransform target, Func<Vector2, Vector2> converter)
            : this(target, SizeDeltaMode.SizeDelta, converter.ToConvert()) { }
        
        public RectTransformSizeDeltaBinder(RectTransform target, SizeDeltaMode mode, Func<Vector2, Vector2> converter)
            : this(target, mode, converter.ToConvert()) { }
        
        public RectTransformSizeDeltaBinder(
            RectTransform target, 
            SizeDeltaMode mode = SizeDeltaMode.SizeDelta, 
            IConverter<Vector2, Vector2>? converter = null)
            : base(target)
        {
            _mode = mode;
            _converter = converter;
        }

        public void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetSizeDelta(value, _mode);
        }

        public void SetValue(int value) =>
            SetValue(new Vector2(value, value));

        public void SetValue(long value) =>
            SetValue(new Vector2(value, value));

        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));

        public void SetValue(double value) =>
            SetValue((float)value);
    }
}