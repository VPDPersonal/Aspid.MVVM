using UnityEngine;
using UnityEngine.EventSystems;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{ 
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{EventTrigger}"/> that sends the cached <see cref="EventTrigger"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(EventTrigger))]
    [AddComponentMenu("Aspid/MVVM/Binders/EventTrigger/EventTrigger To Source Binder")]
    public sealed class EventTriggerToSourceMonoBinder : ComponentToSourceMonoBinder<EventTrigger> { }
}
