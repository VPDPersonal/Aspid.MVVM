using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Transform), "m_LocalRotation")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Euler Angles")]
    [AddComponentContextMenu(typeof(Transform),"Add Transform Binder/Transform Binder - EulerAngles")]
    public partial class TransformEulerAnglesMonoBinder : MonoBinder, IVectorBinder, INumberBinder
    {
        [SerializeField] private Space _space = Space.World;
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
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
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        [BinderLog]
        public void SetValue(Vector3 value) =>
            transform.SetEulerAngles(value, _space, _converter);
    }
}