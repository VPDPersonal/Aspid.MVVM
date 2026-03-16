#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherStringMonoBinder{LocalizeStringEvent}"/> that switches the TableEntryReference
    /// of the component's StringReference between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry Switcher")]
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference", SubPath = "Switcher")]
    public sealed class LocalizeStringEventEntrySwitcherMonoBinder : SwitcherStringMonoBinder<LocalizeStringEvent>
    {
        /// <summary>
        /// Called when applying the selected entry key.
        /// Sets the TableEntryReference of the component's StringReference.
        /// </summary>
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = value;
    }
}
#endif