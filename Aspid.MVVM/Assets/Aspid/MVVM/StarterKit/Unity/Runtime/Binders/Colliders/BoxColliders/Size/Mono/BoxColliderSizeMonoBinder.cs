using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector3MonoBinder{BoxCollider}"/> that binds the <see cref="BoxCollider.size"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current size value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Size")]
    public class BoxColliderSizeMonoBinder : ComponentVector3MonoBinder<BoxCollider>
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.size;
            set => CachedComponent.size = value;
        }
    }
}