using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Collider.material"/> on each element.
    /// </summary>
    /// <remarks>
    /// Reads back <see cref="Collider.sharedMaterial"/>: reading <see cref="Collider.material"/> would clone the asset.
    /// </remarks>
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material EnumGroup")]
    public sealed class ColliderMaterialEnumGroupMonoBinder : EnumGroupMonoBinder<Collider, PhysicsMaterial>
    {
        /// <inheritdoc/>
        protected override void SetValue(Collider element, PhysicsMaterial value) =>
            element.material = value;
    }
}
