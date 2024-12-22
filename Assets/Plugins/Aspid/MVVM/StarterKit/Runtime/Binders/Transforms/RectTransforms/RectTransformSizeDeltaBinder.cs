#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class RectTransformSizeDeltaBinder : Binder, IBinder<Vector2>, INumberBinder
    {
        [Header("Component")]
        [SerializeField] private RectTransform _transform;
        
        [Header("Parameter")]
        [SerializeField] private SizeDeltaMode _mode;

#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Vector2, Vector2>? _converter;

        public RectTransformSizeDeltaBinder(RectTransform transform, Func<Vector2, Vector2> converter)
            : this(transform, SizeDeltaMode.SizeDelta, converter.ToConvert()) { }
        
        public RectTransformSizeDeltaBinder(RectTransform transform, SizeDeltaMode mode, Func<Vector2, Vector2> converter)
            : this(transform, mode, converter.ToConvert()) { }
        
        public RectTransformSizeDeltaBinder(
            RectTransform transform, 
            SizeDeltaMode mode = SizeDeltaMode.SizeDelta, 
            IConverter<Vector2, Vector2>? converter = null)
        {
            _mode = mode;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            _transform.SetSizeDelta(value, _mode);
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