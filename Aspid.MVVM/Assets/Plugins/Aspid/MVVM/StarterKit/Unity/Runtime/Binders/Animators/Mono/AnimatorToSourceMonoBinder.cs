using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator To Source Binder")]
    public sealed class AnimatorToSourceMonoBinder : ComponentToSourceMonoBinder<Animator> { }
}