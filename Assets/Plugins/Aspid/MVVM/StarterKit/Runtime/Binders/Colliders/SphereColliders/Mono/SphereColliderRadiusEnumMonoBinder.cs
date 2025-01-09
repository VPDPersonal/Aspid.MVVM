using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/SphereCollider Binder - Radius Enum")]
    public sealed class SphereColliderRadiusEnumMonoBinder : EnumComponentMonoBinder<SphereCollider, float>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _converter;
#else
        private IConverterFloat _converter;
#endif

        protected override void SetValue(float value) =>
            CachedComponent.radius = _converter?.Convert(value) ?? value;
    }
}