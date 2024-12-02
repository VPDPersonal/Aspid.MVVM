#nullable enable
using System;
using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public class RectTransformSizeDeltaBinder : Binder, IBinder<Vector2>, INumberBinder
    {
        private readonly SizeDeltaMode _mode;
        private readonly RectTransform _transform;
        private readonly IConverter<Vector2, Vector2>? _converter;

        public RectTransformSizeDeltaBinder(RectTransform transform, Func<Vector2, Vector2> converter)
            : this(transform, SizeDeltaMode.SizeDelta, new GenericFuncConverter<Vector2, Vector2>(converter)) { }
        
        public RectTransformSizeDeltaBinder(RectTransform transform, SizeDeltaMode mode, Func<Vector2, Vector2> converter)
            : this(transform, mode, new GenericFuncConverter<Vector2, Vector2>(converter)) { }
        
        public RectTransformSizeDeltaBinder(RectTransform transform, SizeDeltaMode mode = SizeDeltaMode.SizeDelta, IConverter<Vector2, Vector2>? converter = null)
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