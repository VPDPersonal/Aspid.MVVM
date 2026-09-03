using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="SphereCollider.center"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(SphereCollider), serializePropertyNames: "m_Center", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Sphere/SphereCollider Binder – Center Switcher")]
    public sealed class SphereColliderCenterSwitcherMonoBinder : SwitcherMonoBinder<SphereCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value)
        {
            if (this.RequireFinite(value))
                CachedComponent.center = value;
        }
    }
}
