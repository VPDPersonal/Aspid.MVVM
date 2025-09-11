using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Quaternion, UnityEngine.Quaternion>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterQuaternion;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Rotation")]
    [AddPropertyContextMenu(typeof(Transform), "m_LocalRotation")]
    [AddComponentContextMenu(typeof(Transform),"Add Transform Binder/Transform Binder - Rotation")]
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