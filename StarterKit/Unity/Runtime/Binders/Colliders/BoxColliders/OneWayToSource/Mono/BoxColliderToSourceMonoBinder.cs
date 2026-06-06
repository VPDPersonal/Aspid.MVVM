using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{BoxCollider}"/> that sends the cached <see cref="BoxCollider"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(BoxCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider To Source Binder")]
    public sealed class BoxColliderToSourceMonoBinder : ComponentToSourceMonoBinder<BoxCollider> { }
}