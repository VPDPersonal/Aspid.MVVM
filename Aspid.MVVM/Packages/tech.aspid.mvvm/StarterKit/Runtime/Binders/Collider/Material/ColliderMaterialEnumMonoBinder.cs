using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Collider.material"/>.
    /// </summary>
    /// <remarks>
    /// Reads back <see cref="Collider.sharedMaterial"/>: reading <see cref="Collider.material"/> would clone the asset.
    /// </remarks>
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material Enum")]
    public sealed class ColliderMaterialEnumMonoBinder : EnumMonoBinder<Collider, PhysicsMaterial>
    {
        /// <inheritdoc/>
        protected override void SetValue(PhysicsMaterial value) =>
            CachedComponent.material = value;
    }
}
