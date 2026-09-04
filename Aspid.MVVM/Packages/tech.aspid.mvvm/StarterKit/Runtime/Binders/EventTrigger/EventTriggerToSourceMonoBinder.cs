using UnityEngine;
using UnityEngine.EventSystems;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="EventTrigger"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(EventTrigger))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/EventTrigger/EventTrigger To Source Binder")]
    public sealed class EventTriggerToSourceMonoBinder : ComponentToSourceMonoBinder<EventTrigger> { }
}
