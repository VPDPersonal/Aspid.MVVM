using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinderWithConverter{T1, T2}">EnumMonoBinderWithConverter&lt;Collider, PhysicsMaterial&gt;</see> that sets the <see cref="Collider.material"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material Enum")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material", SubPath = "Enum")]
    public sealed class ColliderMaterialEnumMonoBinder : EnumMonoBinderWithConverter<Collider, PhysicsMaterial>
    {
        /// <inheritdoc/>
        protected override void SetValue(PhysicsMaterial value) =>
            CachedComponent.material = value;
    }
}