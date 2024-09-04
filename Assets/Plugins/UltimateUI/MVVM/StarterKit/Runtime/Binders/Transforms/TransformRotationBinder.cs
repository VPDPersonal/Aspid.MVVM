using System;
using UnityEngine;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformRotationBinder : Binder, IRotationBinder
    {
        protected readonly Space Space;
        protected readonly Transform Transform;
        protected readonly IConverter<Quaternion, Quaternion> Converter;
        
        public TransformRotationBinder(Transform transform, Space space = Space.World)
        {
            Space = space;
            Converter = null;
            Transform = transform;
        }
        
        public TransformRotationBinder(Transform transform, Func<Quaternion, Quaternion> converter) 
            : this(transform, Space.World, new GenericFuncConverter<Quaternion, Quaternion>(converter)) { }
        
        public TransformRotationBinder(Transform transform, Space space, Func<Quaternion, Quaternion> converter)
            : this(transform, space, new GenericFuncConverter<Quaternion, Quaternion>(converter)) { }

        public TransformRotationBinder(Transform transform, IConverter<Quaternion, Quaternion> converter) 
            : this(transform, Space.World, converter) { }
        
        public TransformRotationBinder(Transform transform, Space space, IConverter<Quaternion, Quaternion> converter)
        {
            Space = space;
            Transform = transform;
            Converter = converter;
        }
        
        public void SetValue(Quaternion value)
        {
            value = Converter?.Convert(value) ?? value;
            
            switch (Space)
            {
                case Space.Self: Transform.localRotation = value; break;
                case Space.World: Transform.rotation = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }

}