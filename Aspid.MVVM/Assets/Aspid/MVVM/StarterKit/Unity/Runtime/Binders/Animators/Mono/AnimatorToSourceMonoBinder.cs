using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{Animator}"/> that sends the cached <see cref="Animator"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator To Source Binder")]
    public sealed class AnimatorToSourceMonoBinder : ComponentToSourceMonoBinder<Animator> { }
}