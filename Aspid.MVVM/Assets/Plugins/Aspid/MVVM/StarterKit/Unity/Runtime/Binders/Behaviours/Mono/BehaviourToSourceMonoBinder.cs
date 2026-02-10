using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Behaviour))]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour To Source Binder")]
    public sealed class BehaviourToSourceMonoBinder : ComponentToSourceMonoBinder<Behaviour> { }
}