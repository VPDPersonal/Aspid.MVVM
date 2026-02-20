using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(BoxCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider To Source Binder")]
    public sealed class BoxColliderToSourceMonoBinder : ComponentToSourceMonoBinder<BoxCollider> { }
}