using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="BoxCollider"/> component
    /// back to the ViewModel in one-way-to-source mode when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(BoxCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider To Source Binder")]
    public sealed class BoxColliderToSourceMonoBinder : ComponentToSourceMonoBinder<BoxCollider> { }
}
