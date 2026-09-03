using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="CapsuleCollider"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(CapsuleCollider))]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Capsule/CapsuleCollider To Source Binder")]
    public sealed class CapsuleColliderToSourceMonoBinder : ComponentToSourceMonoBinder<CapsuleCollider> { }
}
