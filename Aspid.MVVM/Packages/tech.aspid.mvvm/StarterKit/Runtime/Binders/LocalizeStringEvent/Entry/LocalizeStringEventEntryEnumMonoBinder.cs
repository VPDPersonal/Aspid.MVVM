#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets
    /// the <see cref="LocalizedString.TableEntryReference"/> of <see cref="LocalizeStringEvent.StringReference"/>
    /// by key name.
    /// </summary>
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry Enum")]
    public sealed class LocalizeStringEventEntryEnumMonoBinder : EnumMonoBinder<LocalizeStringEvent, string>
    {
        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = value;
    }
}
#endif
