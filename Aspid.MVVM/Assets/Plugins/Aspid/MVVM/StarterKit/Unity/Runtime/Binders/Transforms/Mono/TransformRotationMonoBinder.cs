using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Quaternion, UnityEngine.Quaternion>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterQuaternion;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Rotation")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation")]
    public partial class TransformRotationMonoBinder : ComponentMonoBinder<Transform, Quaternion, Converter>,
        INumberBinder,
        IRotationBinder
    {
        [SerializeField] private Space _space = Space.World;
        
        protected sealed override Quaternion Property
        {
            get => transform.GetRotation(_space);
            set => transform.SetRotation(value, _space);
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
            base.SetValue(Quaternion.Euler(new Vector3(value, value, value)));
    }
}