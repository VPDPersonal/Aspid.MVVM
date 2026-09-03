#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches
    /// the <see cref="LocalizedString.TableEntryReference"/> of <see cref="LocalizeStringEvent.StringReference"/>
    /// by key name.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(
        typeof(LocalizeStringEvent),
        serializePropertyNames: "m_StringReference",
        SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry Switcher")]
    public sealed class LocalizeStringEventEntrySwitcherMonoBinder : SwitcherMonoBinder<LocalizeStringEvent, string>
    {
        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = value;
    }
}
#endif
