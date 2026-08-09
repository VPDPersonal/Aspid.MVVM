using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Mesh, UnityEngine.Mesh>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{MeshCollider, Mesh, IConverter{Mesh, Mesh}}"/> that sets the <see cref="MeshCollider.sharedMesh"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Mesh/MeshCollider Binder – Mesh EnumGroup")]
    [AddBinderContextMenu(typeof(MeshCollider), serializePropertyNames: "m_Mesh", SubPath = "EnumGroup")]
    public sealed class MeshColliderMeshEnumGroupMonoBinder : EnumGroupMonoBinder<MeshCollider, Mesh, Converter>
    {
        /// <inheritdoc/>
        protected override void SetValue(MeshCollider element, Mesh value) =>
            element.sharedMesh = value;
    }
}