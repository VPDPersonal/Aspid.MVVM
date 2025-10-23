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
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider Binder - Radius")]
    [AddComponentContextMenu(typeof(CapsuleCollider),"Add CapsuleCollider Binder/CapsuleCollider Binder - Radius")]
    public partial class CapsuleColliderRadiusMonoBinder : ComponentMonoBinder<CapsuleCollider>, INumberBinder
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