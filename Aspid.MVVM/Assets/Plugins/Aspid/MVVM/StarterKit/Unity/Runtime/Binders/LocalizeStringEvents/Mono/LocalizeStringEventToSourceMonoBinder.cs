#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(LocalizeStringEvent))]
    [AddComponentMenu("Aspid/MVVM/Binders/LocalizeStringEvent/LocalizeStringEvent To Source Binder")]
    public class LocalizeStringEventToSourceMonoBinder : ComponentToSourceMonoBinder<LocalizeStringEvent> { }
}
#endif