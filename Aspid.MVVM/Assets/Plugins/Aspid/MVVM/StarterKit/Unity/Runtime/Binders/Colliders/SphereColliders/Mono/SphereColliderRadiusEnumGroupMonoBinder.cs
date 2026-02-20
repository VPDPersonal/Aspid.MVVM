using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Radius EnumGroup")]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Radius", SubPath = "EnumGroup")]
    public sealed class SphereColliderRadiusEnumGroupMonoBinder : EnumGroupMonoBinder<SphereCollider, float, Converter>
    {
        protected override void SetValue(SphereCollider element, float value) =>
            element.radius = value;
    }
}