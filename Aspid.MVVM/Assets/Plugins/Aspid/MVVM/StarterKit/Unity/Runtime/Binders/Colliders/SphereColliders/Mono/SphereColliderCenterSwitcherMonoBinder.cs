using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that switches the <see cref="SphereCollider.center"/> property on a <see cref="SphereCollider"/>
    /// between two values based on a bound boolean ViewModel property.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Center Switcher")]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Center", SubPath = "Switcher")]
    public sealed class SphereColliderCenterSwitcherMonoBinder : SwitcherMonoBinder<SphereCollider, Vector3, Converter>
    {
        protected override void SetValue(Vector3 value) =>
            CachedComponent.center = value;
    }
}