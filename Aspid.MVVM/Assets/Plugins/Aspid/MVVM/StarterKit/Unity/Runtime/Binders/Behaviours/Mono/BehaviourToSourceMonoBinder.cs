using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{Behaviour}"/> that sends the cached <see cref="Behaviour"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Behaviour))]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour To Source Binder")]
    public sealed class BehaviourToSourceMonoBinder : ComponentToSourceMonoBinder<Behaviour> { }
}