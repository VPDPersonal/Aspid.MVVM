using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{SphereCollider}"/> that switches the <see cref="SphereCollider.radius"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Radius Switcher")]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Radius", SubPath = "Switcher")]
    public sealed class SphereColliderRadiusSwitcherMonoBinder : SwitcherFloatMonoBinder<SphereCollider>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.radius = value;
    }
}