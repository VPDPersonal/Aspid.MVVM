using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinderWithConverter{T1, T2}">SwitcherMonoBinderWithConverter&lt;MeshCollider, Mesh&gt;</see> that switches the <see cref="MeshCollider.sharedMesh"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh Switcher")]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh", SubPath = "Switcher")]
    public sealed class MeshColliderMeshSwitcherMonoBinder : SwitcherMonoBinderWithConverter<MeshCollider, Mesh>
    {
        /// <inheritdoc/>
        protected override void SetValue(Mesh value) =>
            CachedComponent.sharedMesh = value;
    }
}