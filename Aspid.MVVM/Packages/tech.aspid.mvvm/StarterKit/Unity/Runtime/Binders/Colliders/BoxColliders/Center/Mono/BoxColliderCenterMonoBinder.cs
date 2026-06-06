using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector3MonoBinder{BoxCollider}"/> that binds the <see cref="BoxCollider.center"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current center value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Center")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Center")]
    public class BoxColliderCenterMonoBinder : ComponentVector3MonoBinder<BoxCollider>
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.center;
            set => CachedComponent.center = value;
        }
    }
}