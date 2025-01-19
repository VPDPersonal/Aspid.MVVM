using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/Box/BoxCollider Binder - Size")]
    public partial class BoxColliderSizeMonoBinder : ComponentMonoBinder<BoxCollider>, IVectorBinder, INumberBinder
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        [BinderLog]
        public void SetValue(Vector3 value) =>
            CachedComponent.size = _converter.Convert(value, CachedComponent.size);
        
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