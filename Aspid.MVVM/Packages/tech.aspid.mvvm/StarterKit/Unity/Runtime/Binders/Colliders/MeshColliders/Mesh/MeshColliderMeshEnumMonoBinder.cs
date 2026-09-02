using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{T1, T2}">EnumMonoBinder&lt;MeshCollider, Mesh&gt;</see> that sets the <see cref="MeshCollider.sharedMesh"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh Enum")]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh", SubPath = "Enum")]
    public sealed class MeshColliderMeshEnumMonoBinder : EnumMonoBinder<MeshCollider, Mesh>
    {
        /// <inheritdoc/>
        protected override void SetValue(Mesh value) =>
            CachedComponent.sharedMesh = value;
    }
}