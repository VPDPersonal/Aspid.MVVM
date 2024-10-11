#nullable enable
using System;
using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformEulerAnglesBinder : Binder, IVectorBinder
    {
        private readonly Space _space;
        private readonly Transform _transform;
        private readonly IConverter<Vector3, Vector3>? _converter;
        
        public TransformEulerAnglesBinder(Transform transform, Space space = Space.World)
        {
            _space = space;
            _converter = null;
            _transform = transform;
        }
        
        public TransformEulerAnglesBinder(Transform transform, Func<Vector3, Vector3> converter) 
            : this(transform, Space.World, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public TransformEulerAnglesBinder(Transform transform, Space space, Func<Vector3, Vector3> converter)
            : this(transform, space, new GenericFuncConverter<Vector3, Vector3>(converter)) { }

        public TransformEulerAnglesBinder(Transform transform, IConverter<Vector3, Vector3>? converter) :
            this(transform, Space.World, converter) { }
        
        public TransformEulerAnglesBinder(Transform transform, Space space, IConverter<Vector3, Vector3>? converter)
        {
            _space = space;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }
        
        public void SetValue(Vector2 value) => SetValue((Vector3)value);
        
        public void SetValue(Vector3 value)
        {
            value = _converter?.Convert(value) ?? value;
            
            switch (_space)
            {
                case Space.Self: _transform.localEulerAngles = value; break;
                case Space.World: _transform.eulerAngles = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }

}