#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentStringMonoBinder{LocalizeStringEvent}"/> that sets the TableEntryReference
    /// of the component's StringReference when the bound ViewModel string value changes.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current
    /// table entry reference is immediately forwarded to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry")]
    public class LocalizeStringEventEntryMonoBinder : ComponentStringMonoBinder<LocalizeStringEvent>
    {
        /// <inheritdoc/>
        protected sealed override string Property
        {
            get => CachedComponent.StringReference.TableEntryReference;
            set => CachedComponent.StringReference.TableEntryReference = value;
        }
    }
}
#endif