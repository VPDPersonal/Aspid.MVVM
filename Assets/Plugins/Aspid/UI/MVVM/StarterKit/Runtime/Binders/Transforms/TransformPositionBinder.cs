#nullable enable
using System;
using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public class TransformPositionBinder : Binder, IVectorBinder
    {
        private readonly Space _space;
        private readonly VectorMode _mode;
        private readonly Transform _transform;
        private readonly IConverter<Vector3, Vector3>? _converter;
        
        public TransformPositionBinder(Transform transform, Space space = Space.World, VectorMode mode = VectorMode.XYZ)
        {
            _mode = mode;
            _space = space;
            _converter = null;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }
        
        public TransformPositionBinder(Transform transform, Func<Vector3, Vector3> converter) 
            : this(transform, Space.World, VectorMode.XYZ, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public TransformPositionBinder(Transform transform, Space space, VectorMode mode, Func<Vector3, Vector3> converter)
            : this(transform, space, mode, new GenericFuncConverter<Vector3, Vector3>(converter)) { }

        public TransformPositionBinder(Transform transform, IConverter<Vector3, Vector3>? converter) :
            this(transform, Space.World, VectorMode.XYZ, converter) { }
        
        public TransformPositionBinder(Transform transform, Space space, VectorMode mode, IConverter<Vector3, Vector3>? converter)
        {
            _mode = mode;
            _space = space;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }
        
        public void SetValue(Vector2 value) => SetValue((Vector3)value);
        
        public void SetValue(Vector3 value)
        {
            value = _converter?.Convert(value) ?? value;
            _transform.SetPosition(value, _mode, _space);
        }
    }
}