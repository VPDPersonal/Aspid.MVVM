#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that reads the current state of a <see cref="LocalizeStringEvent"/> component
    /// back to the ViewModel in one-way-to-source mode.
    /// </summary>
    [AddBinderContextMenu(typeof(LocalizeStringEvent))]
    [AddComponentMenu("Aspid/MVVM/Binders/LocalizeStringEvent/LocalizeStringEvent To Source Binder")]
    public class LocalizeStringEventToSourceMonoBinder : ComponentToSourceMonoBinder<LocalizeStringEvent> { }
}
#endif