using System;
using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Euler Angles")]
    public partial class TransformEulerAnglesMonoBinder : MonoBinder, IVectorBinder
    {
        [field: Header("Parameter")]
        [field: SerializeField] 
        protected Space Space { get; private set; }
        
        [field: Header("Converter")]
        [field: SerializeReference]
#if ULTIMATE_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
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
                case Space.Self: transform.localEulerAngles = value; break;
                case Space.World: transform.eulerAngles = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}