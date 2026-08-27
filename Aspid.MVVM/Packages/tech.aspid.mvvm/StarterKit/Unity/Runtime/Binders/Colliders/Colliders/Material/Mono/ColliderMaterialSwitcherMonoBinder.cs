using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinderWithConverter{T1, T2}">SwitcherMonoBinderWithConverter&lt;Collider, PhysicsMaterial&gt;</see> that switches the <see cref="Collider.material"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material Switcher")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material", SubPath = "Switcher")]
    public sealed class ColliderMaterialSwitcherMonoBinder : SwitcherMonoBinderWithConverter<Collider, PhysicsMaterial>
    {
        /// <inheritdoc/>
        protected override void SetValue(PhysicsMaterial value) =>
            CachedComponent.material = value;
    }
}