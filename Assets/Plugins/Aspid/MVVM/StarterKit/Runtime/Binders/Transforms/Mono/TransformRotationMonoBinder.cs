using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Generation;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Quaternion, UnityEngine.Quaternion>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterQuaternion;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Rotation")]
    public partial class TransformRotationMonoBinder : MonoBinder, IRotationBinder, INumberBinder
    {
        [SerializeField] private Space _space = Space.World;

        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
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
            transform.SetRotation(value, _space);
        }
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);
        
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
        
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector3(value, value, value));
    }
}