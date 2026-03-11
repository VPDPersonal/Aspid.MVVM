using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumVector3MonoBinder{BoxCollider}"/> that sets the <see cref="BoxCollider.size"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Size Enum")]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Size", SubPath = "Enum")]
    public sealed class BoxColliderSizeEnumMonoBinder : EnumVector3MonoBinder<BoxCollider>
    {
        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.size = value;
    }
}