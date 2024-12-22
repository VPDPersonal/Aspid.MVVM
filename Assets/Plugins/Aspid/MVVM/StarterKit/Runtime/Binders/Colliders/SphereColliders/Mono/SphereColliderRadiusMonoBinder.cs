using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Collider/SphereCollider Binder - Radius")]
    public class SphereColliderRadiusMonoBinder : ComponentMonoBinder<SphereCollider>, INumberBinder
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _converter;
#else
        private IConverterVector3 _converter;
#endif

        public void SetValue(int value) =>
            SetValue((float)value);

        public void SetValue(long value) =>
            SetValue((float)value);
        
        public void SetValue(float value) =>
            CachedComponent.radius = _converter?.Convert(value) ?? value;

        public void SetValue(double value) =>
            SetValue((float)value);
    }

}