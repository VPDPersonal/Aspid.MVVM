#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{LocalizeStringEvent}"/> that sends the current bound property value
    /// of a <see cref="LocalizeStringEvent"/> back to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(LocalizeStringEvent))]
    [AddComponentMenu("Aspid/MVVM/Binders/LocalizeStringEvent/LocalizeStringEvent To Source Binder")]
    public class LocalizeStringEventToSourceMonoBinder : ComponentToSourceMonoBinder<LocalizeStringEvent> { }
}
#endif