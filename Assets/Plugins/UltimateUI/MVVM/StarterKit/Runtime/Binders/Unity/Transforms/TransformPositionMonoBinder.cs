using System;
using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Position")]
    public partial class TransformPositionMonoBinder : MonoBinder, IVectorBinder
    {
        [field: Header("Parameter")]
        [field: SerializeField] 
        protected Space Space { get; private set; }
        
        [field: Header("Converter")]
        [field: SerializeReference]
        protected IConverterVector3ToVector3 Converter { get; private set; }

        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        [BinderLog]
        public void SetValue(Vector3 value)
        {
            value = Converter?.Convert(value) ?? value;
            
            switch (Space)
            {
                case Space.Self: transform.localPosition = value; break;
                case Space.World: transform.position = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}