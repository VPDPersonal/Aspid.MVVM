#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class RectTransformAnchoredPositionBinder : Binder, IVectorBinder
    {
        private readonly Space _space;
        private readonly VectorMode _mode;
        private readonly RectTransform _transform;
        private readonly IConverter<Vector3, Vector3>? _converter;
        
        public RectTransformAnchoredPositionBinder(RectTransform transform, Func<Vector3, Vector3> converter)
            : this(transform, Space.World, VectorMode.XYZ, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public RectTransformAnchoredPositionBinder(RectTransform transform, Space space, VectorMode mode, Func<Vector3, Vector3> converter)
            : this(transform, space, mode, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public RectTransformAnchoredPositionBinder(RectTransform transform, Space space = Space.World, VectorMode mode = VectorMode.XYZ, IConverter<Vector3, Vector3>? converter = null)
        {
            _mode = mode;
            _space = space;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        public void SetValue(Vector3 value)
        {
            value = _converter?.Convert(value) ?? value;
            _transform.SetAnchoredPosition(value, _mode, _space);
        }
    }

}