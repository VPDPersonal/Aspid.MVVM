using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder - Center")]
    public class SphereColliderCenterMonoBinder : ComponentMonoBinder<SphereCollider>, IVectorBinder, INumberBinder
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        public void SetValue(Vector3 value) =>
            CachedComponent.center = _converter.Convert(value, CachedComponent.center);
        
        public void SetValue(int value) =>
            SetValue(new Vector3(value, value, value));

        public void SetValue(long value) =>
            SetValue(new Vector3(value, value, value));

        public void SetValue(float value) =>
            SetValue(new Vector3(value, value, value));

        public void SetValue(double value) =>
            SetValue((float)value);
    }
}