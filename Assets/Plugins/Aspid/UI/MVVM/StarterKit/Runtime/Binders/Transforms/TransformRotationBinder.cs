#nullable enable
using System;
using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformRotationBinder : Binder, IRotationBinder
    {
        private readonly Space _space;
        private readonly Transform _transform;
        private readonly IConverter<Quaternion, Quaternion>? _converter;
        
        public TransformRotationBinder(Transform transform, Space space = Space.World)
        {
            _space = space;
            _converter = null;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }
        
        public TransformRotationBinder(Transform transform, Func<Quaternion, Quaternion> converter) 
            : this(transform, Space.World, new GenericFuncConverter<Quaternion, Quaternion>(converter)) { }
        
        public TransformRotationBinder(Transform transform, Space space, Func<Quaternion, Quaternion> converter)
            : this(transform, space, new GenericFuncConverter<Quaternion, Quaternion>(converter)) { }

        public TransformRotationBinder(Transform transform, IConverter<Quaternion, Quaternion>? converter) 
            : this(transform, Space.World, converter) { }
        
        public TransformRotationBinder(Transform transform, Space space, IConverter<Quaternion, Quaternion>? converter)
        {
            _space = space;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }
        
        public void SetValue(Quaternion value)
        {
            value = _converter?.Convert(value) ?? value;
            
            switch (_space)
            {
                case Space.Self: _transform.localRotation = value; break;
                case Space.World: _transform.rotation = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }

}