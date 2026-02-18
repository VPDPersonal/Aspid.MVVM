using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Radius Switcher")]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Radius", SubPath = "Switcher")]
    public sealed class SphereColliderRadiusSwitcherMonoBinder : SwitcherMonoBinder<SphereCollider, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.radius = value;
    }
}