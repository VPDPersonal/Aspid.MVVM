using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(CapsuleCollider), "m_Radius")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder - Radius Switcher")]
    [AddComponentContextMenu(typeof(CapsuleCollider),"Add CapsuleCollider Binder/CapsuleCollider Binder - Radius Switcher")]
    public sealed class CapsuleColliderRadiusSwitcherMonoBinder : SwitcherMonoBinder<CapsuleCollider, float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(float value) =>
            CachedComponent.radius = _converter?.Convert(value) ?? value;
    }
}