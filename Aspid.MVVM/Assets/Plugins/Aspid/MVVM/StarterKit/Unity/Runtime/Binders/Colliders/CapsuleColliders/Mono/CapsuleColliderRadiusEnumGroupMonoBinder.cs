using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Radius EnumGroup")]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Radius", SubPath = "EnumGroup")]
    public sealed class CapsuleColliderRadiusEnumGroupMonoBinder : EnumGroupMonoBinder<CapsuleCollider, float, Converter>
    {
        protected override void SetValue(CapsuleCollider element, float value) =>
            element.radius = value;
    }
}