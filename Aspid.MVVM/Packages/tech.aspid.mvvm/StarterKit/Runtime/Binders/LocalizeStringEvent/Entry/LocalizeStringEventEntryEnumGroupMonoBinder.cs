#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets
    /// the <see cref="LocalizedString.TableEntryReference"/> of <see cref="LocalizeStringEvent.StringReference"/>
    /// by key name on each element.
    /// </summary>
    [AddBinderContextMenu(
        typeof(LocalizeStringEvent),
        serializePropertyNames: "m_StringReference",
        SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry EnumGroup")]
    public sealed class LocalizeStringEventEntryEnumGroupMonoBinder : EnumGroupMonoBinder<LocalizeStringEvent, string>
    {
        /// <inheritdoc/>
        protected override void SetValue(LocalizeStringEvent element, string value) =>
            element.StringReference.TableEntryReference = value;
    }
}
#endif
