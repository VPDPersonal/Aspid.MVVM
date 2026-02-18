using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder – Center EnumGroup")]
    [AddBinderContextMenu(typeof(CapsuleCollider), serializePropertyNames: "m_Center", SubPath = "EnumGroup")]
    public sealed class CapsuleColliderCenterEnumGroupMonoBinder : EnumGroupMonoBinder<CapsuleCollider, Vector3, Converter>
    {
        protected override void SetValue(CapsuleCollider element, Vector3 value) =>
            element.center = value;
    }
}