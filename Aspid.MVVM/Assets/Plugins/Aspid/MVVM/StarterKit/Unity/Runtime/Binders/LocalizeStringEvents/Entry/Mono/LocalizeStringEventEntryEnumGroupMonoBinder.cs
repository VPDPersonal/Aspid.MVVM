#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupStringMonoBinder{LocalizeStringEvent}"/> that sets the <c>TableEntryReference</c>
    /// of the <c>StringReference</c> on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry EnumGroup")]
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference", SubPath = "EnumGroup")]
    public sealed class LocalizeStringEventEntryEnumGroupMonoBinder : EnumGroupStringMonoBinder<LocalizeStringEvent>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets the <c>TableEntryReference</c> of the element's <c>StringReference</c>.
        /// </summary>
        protected override void SetValue(LocalizeStringEvent element, string value) =>
            element.StringReference.TableEntryReference = value;
    }
}
#endif