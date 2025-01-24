using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder - Radius")]
    public class CapsuleColliderRadiusMonoBinder : ComponentMonoBinder<CapsuleCollider>, INumberBinder
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

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