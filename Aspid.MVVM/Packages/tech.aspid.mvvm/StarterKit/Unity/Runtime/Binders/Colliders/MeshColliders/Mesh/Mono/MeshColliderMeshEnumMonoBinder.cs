using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Mesh, UnityEngine.Mesh>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{T1, T2, T3}">EnumMonoBinder&lt;MeshCollider, Mesh, IConverter&lt;Mesh, Mesh&gt;&gt;</see> that sets the <see cref="MeshCollider.sharedMesh"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh Enum")]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh", SubPath = "Enum")]
    public sealed class MeshColliderMeshEnumMonoBinder : EnumMonoBinder<MeshCollider, Mesh, Converter>
    {
        /// <inheritdoc/>
        protected override void SetValue(Mesh value) =>
            CachedComponent.sharedMesh = value;
    }
}