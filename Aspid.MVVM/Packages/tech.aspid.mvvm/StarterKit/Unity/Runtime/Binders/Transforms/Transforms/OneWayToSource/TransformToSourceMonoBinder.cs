using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{Transform}"/> that sends the cached <see cref="Transform"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Transform))]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform To Source Binder")]
    public sealed class TransformToSourceMonoBinder : ComponentToSourceMonoBinder<Transform> { }
}