#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumStringMonoBinder{LocalizeStringEvent}"/> that sets the <c>TableEntryReference</c>
    /// of the component's <c>StringReference</c> based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry Enum")]
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference", SubPath = "Enum")]
    public sealed class LocalizeStringEventEntryEnumMonoBinder : EnumStringMonoBinder<LocalizeStringEvent>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets the <c>TableEntryReference</c> of the component's <c>StringReference</c>.
        /// </summary>
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = value;
    }
}
#endif