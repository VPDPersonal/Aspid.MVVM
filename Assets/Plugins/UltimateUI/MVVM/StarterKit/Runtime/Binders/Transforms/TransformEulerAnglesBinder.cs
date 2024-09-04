using System;
using UnityEngine;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformEulerAnglesBinder : Binder, IVectorBinder
    {
        protected readonly Space Space;
        protected readonly Transform Transform;
        protected readonly IConverter<Vector3, Vector3> Converter;
        
        public TransformEulerAnglesBinder(Transform transform, Space space = Space.World)
        {
            Space = space;
            Converter = null;
            Transform = transform;
        }
        
        public TransformEulerAnglesBinder(Transform transform, Func<Vector3, Vector3> converter) 
            : this(transform, Space.World, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public TransformEulerAnglesBinder(Transform transform, Space space, Func<Vector3, Vector3> converter)
            : this(transform, space, new GenericFuncConverter<Vector3, Vector3>(converter)) { }

        public TransformEulerAnglesBinder(Transform transform, IConverter<Vector3, Vector3> converter) :
            this(transform, Space.World, converter) { }
        
        public TransformEulerAnglesBinder(Transform transform, Space space, IConverter<Vector3, Vector3> converter)
        {
            Space = space;
            Transform = transform;
            Converter = converter;
        }

        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        [BinderLog]
        public void SetValue(Vector3 value)
        {
            value = Converter?.Convert(value) ?? value;
            
            switch (Space)
            {
                case Space.Self: Transform.localEulerAngles = value; break;
                case Space.World: Transform.eulerAngles = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }

}