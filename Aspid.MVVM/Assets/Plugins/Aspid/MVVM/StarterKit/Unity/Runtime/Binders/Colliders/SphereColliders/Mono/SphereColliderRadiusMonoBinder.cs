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
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder - Radius")]
    [AddComponentContextMenu(typeof(SphereCollider),"Add SphereCollider Binder/SphereCollider Binder - Radius")]
    public partial class SphereColliderRadiusMonoBinder : ComponentMonoBinder<SphereCollider>, INumberBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);
        
        [BinderLog]
        public void SetValue(float value) =>
            CachedComponent.radius = _converter?.Convert(value) ?? value;

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }

}