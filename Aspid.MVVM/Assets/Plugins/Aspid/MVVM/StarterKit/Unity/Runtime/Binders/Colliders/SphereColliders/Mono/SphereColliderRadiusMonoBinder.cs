using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Radius")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Radius")]
    public class SphereColliderRadiusMonoBinder : ComponentFloatMonoBinder<SphereCollider>
    {
        protected sealed override float Property
        {
            get =>  CachedComponent.radius;
            set => CachedComponent.radius = value;
        }
    }
}