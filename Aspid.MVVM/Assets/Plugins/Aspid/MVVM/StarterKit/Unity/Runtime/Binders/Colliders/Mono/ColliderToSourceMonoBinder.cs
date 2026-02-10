using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Collider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider To Source Binder")]
    public sealed class ColliderToSourceMonoBinder : ComponentToSourceMonoBinder<Collider> { }
}