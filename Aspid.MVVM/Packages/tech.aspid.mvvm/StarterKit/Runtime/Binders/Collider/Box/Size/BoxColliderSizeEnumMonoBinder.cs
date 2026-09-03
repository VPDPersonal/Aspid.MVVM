using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="BoxCollider.size"/>.
    /// </summary>
    /// <remarks>
    /// Negative components are raised to zero.
    /// </remarks>
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Size", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Size Enum")]
    public sealed class BoxColliderSizeEnumMonoBinder : EnumMonoBinder<BoxCollider, Vector3>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.size = this.NonNegative(value);
    }
}
