#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent,TValue}">EnumMonoBinder&lt;LocalizeStringEvent, string&gt;</see> that sets the TableEntryReference
    /// of the component's StringReference based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry Enum")]
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference", SubPath = "Enum")]
    public sealed class LocalizeStringEventEntryEnumMonoBinder : EnumMonoBinder<LocalizeStringEvent, string>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets the TableEntryReference of the component's StringReference.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = value;
    }
}
#endif