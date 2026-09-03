#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="LocalizeStringEvent"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(LocalizeStringEvent))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent To Source Binder")]
    public sealed class LocalizeStringEventToSourceMonoBinder : ComponentToSourceMonoBinder<LocalizeStringEvent> { }
}
#endif
