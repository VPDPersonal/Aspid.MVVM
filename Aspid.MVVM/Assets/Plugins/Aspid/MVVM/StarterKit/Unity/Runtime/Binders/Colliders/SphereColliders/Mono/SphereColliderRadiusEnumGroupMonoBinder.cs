using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="SphereCollider.radius"/> property on a group of <see cref="SphereCollider"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Radius EnumGroup")]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Radius", SubPath = "EnumGroup")]
    public sealed class SphereColliderRadiusEnumGroupMonoBinder : EnumGroupMonoBinder<SphereCollider, float, Converter>
    {
        protected override void SetValue(SphereCollider element, float value) =>
            element.radius = value;
    }
}