using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(SphereCollider), "m_Radius")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder - Radius Switcher")]
    [AddComponentContextMenu(typeof(SphereCollider),"Add SphereCollider Binder/SphereCollider Binder - Radius Switcher")]
    public sealed class SphereColliderRadiusSwitcherMonoBinder : SwitcherMonoBinder<SphereCollider, float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        protected override void SetValue(float value) =>
            CachedComponent.radius = _converter?.Convert(value) ?? value;
    }
}