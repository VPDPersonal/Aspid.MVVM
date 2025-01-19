using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/Box/BoxCollider Binder - Center")]
    public partial class BoxColliderCenterMonoBinder : ComponentMonoBinder<BoxCollider>, IVectorBinder, INumberBinder
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        [BinderLog]
        public void SetValue(Vector3 value) =>
            CachedComponent.center = _converter.Convert(value, CachedComponent.center);

        [BinderLog]
        public void SetValue(int value) =>
            SetValue(new Vector3(value, value, value));

        [BinderLog]
        public void SetValue(long value) =>
            SetValue(new Vector3(value, value, value));

        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector3(value, value, value));

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}