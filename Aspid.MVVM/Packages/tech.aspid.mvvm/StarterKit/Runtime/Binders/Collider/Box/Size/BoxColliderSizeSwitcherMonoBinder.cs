using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="BoxCollider.size"/>.
    /// </summary>
    /// <remarks>
    /// Negative components are raised to zero.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Size", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Size Switcher")]
    public sealed class BoxColliderSizeSwitcherMonoBinder : SwitcherMonoBinder<BoxCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.size = this.NonNegative(value);
    }
}
