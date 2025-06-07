using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(SphereCollider), "m_Radius")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder - Radius Enum")]
    [AddComponentContextMenu(typeof(SphereCollider),"Add SphereCollider Binder/SphereCollider Binder - Radius Enum")]
    public sealed class SphereColliderRadiusEnumMonoBinder : EnumComponentMonoBinder<SphereCollider, float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        protected override void SetValue(float value) =>
            CachedComponent.radius = _converter?.Convert(value) ?? value;
    }
}