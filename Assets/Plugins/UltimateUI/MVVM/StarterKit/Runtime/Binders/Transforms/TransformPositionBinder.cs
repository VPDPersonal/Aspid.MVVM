using System;
using UnityEngine;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformPositionBinder : Binder, IVectorBinder
    {
        protected readonly Space Space;
        protected readonly Transform Transform;
        protected readonly IConverter<Vector3, Vector3> Converter;
        
        public TransformPositionBinder(Transform transform, Space space = Space.World)
        {
            Space = space;
            Converter = null;
            Transform = transform;
        }
        
        public TransformPositionBinder(Transform transform, Func<Vector3, Vector3> converter) 
            : this(transform, Space.World, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public TransformPositionBinder(Transform transform, Space space, Func<Vector3, Vector3> converter)
            : this(transform, space, new GenericFuncConverter<Vector3, Vector3>(converter)) { }

        public TransformPositionBinder(Transform transform, IConverter<Vector3, Vector3> converter) :
            this(transform, Space.World, converter) { }
        
        public TransformPositionBinder(Transform transform, Space space, IConverter<Vector3, Vector3> converter)
        {
            Space = space;
            Transform = transform;
            Converter = converter;
        }
        
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        public void SetValue(Vector3 value)
        {
            value = Converter?.Convert(value) ?? value;
            
            switch (Space)
            {
                case Space.Self: Transform.localPosition = value; break;
                case Space.World: Transform.position = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}