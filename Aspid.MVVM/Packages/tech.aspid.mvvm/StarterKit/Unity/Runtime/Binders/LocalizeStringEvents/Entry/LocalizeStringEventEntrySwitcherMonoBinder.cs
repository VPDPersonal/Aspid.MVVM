#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent,T}">SwitcherMonoBinder&lt;LocalizeStringEvent, string&gt;</see> that switches the TableEntryReference
    /// of the component's StringReference between two values based on the bound boolean ViewModel value.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry Switcher")]
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference", SubPath = "Switcher")]
    public sealed class LocalizeStringEventEntrySwitcherMonoBinder : SwitcherMonoBinder<LocalizeStringEvent, string>
    {
        /// <summary>
        /// Called when applying the selected entry key.
        /// Sets the TableEntryReference of the component's StringReference.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = value;
    }
}
#endif