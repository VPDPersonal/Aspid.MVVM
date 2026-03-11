using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{Collider}"/> that sends the cached <see cref="Collider"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider To Source Binder")]
    public sealed class ColliderToSourceMonoBinder : ComponentToSourceMonoBinder<Collider> { }
}