using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent,TValue}">EnumMonoBinder&lt;SphereCollider, float&gt;</see> that sets the <see cref="SphereCollider.radius"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Radius Enum")]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Radius", SubPath = "Enum")]
    public sealed class SphereColliderRadiusEnumMonoBinder : EnumMonoBinder<SphereCollider, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.radius = this.NonNegative(value);
    }
}