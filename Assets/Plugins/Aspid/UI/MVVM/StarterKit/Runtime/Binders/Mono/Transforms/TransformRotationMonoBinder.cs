using System;
using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Rotation")]
    public partial class TransformRotationMonoBinder : Aspid.UI.MVVM.Mono.MonoBinder, IRotationBinder
    {
        [Header("Parameter")]
        [SerializeField] private Space _space;

        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterQuaternionToQuaternion _converter;
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        [BinderLog]
        public void SetValue(Vector3 value) =>
            SetValue(Quaternion.Euler(value));
        
        [BinderLog]
        public void SetValue(Quaternion value)
        {
            value = _converter?.Convert(value) ?? value;
            
            switch (_space)
            {
                case Space.Self: transform.localRotation = value; break;
                case Space.World: transform.rotation = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}