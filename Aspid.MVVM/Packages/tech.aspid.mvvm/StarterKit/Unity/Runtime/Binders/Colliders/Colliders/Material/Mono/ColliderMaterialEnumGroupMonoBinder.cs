using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinderWithConverter{T1, T2}">EnumGroupMonoBinderWithConverter&lt;Collider, PhysicsMaterial&gt;</see> that sets the <see cref="Collider.material"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material EnumGroup")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material", SubPath = "EnumGroup")]
    public sealed class ColliderMaterialEnumGroupMonoBinder : EnumGroupMonoBinderWithConverter<Collider, PhysicsMaterial>
    {
        /// <inheritdoc/>
        protected override void SetValue(Collider element, PhysicsMaterial value) =>
            element.material = value;
    }
}