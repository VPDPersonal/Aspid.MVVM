using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of an <see cref="Animator"/> component
    /// back to the ViewModel in one-way-to-source mode.
    /// </summary>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator To Source Binder")]
    public sealed class AnimatorToSourceMonoBinder : ComponentToSourceMonoBinder<Animator> { }
}