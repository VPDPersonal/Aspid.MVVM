using System;
using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Rotation")]
    public partial class TransformRotationMonoBinder : MonoBinder, IRotationBinder
    {
        [field: Header("Parameter")]
        [field: SerializeField] 
        protected Space Space { get; private set; }
        
        [field: Header("Converter")]
        [field: SerializeReference]
#if ULTIMATE_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterQuaternionToQuaternion Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        [BinderLog]
        public void SetValue(Vector3 value) =>
            SetValue(Quaternion.Euler(value));
        
        [BinderLog]
        public void SetValue(Quaternion value)
        {
            value = Converter?.Convert(value) ?? value;
            
            switch (Space)
            {
                case Space.Self: transform.localRotation = value; break;
                case Space.World: transform.rotation = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}