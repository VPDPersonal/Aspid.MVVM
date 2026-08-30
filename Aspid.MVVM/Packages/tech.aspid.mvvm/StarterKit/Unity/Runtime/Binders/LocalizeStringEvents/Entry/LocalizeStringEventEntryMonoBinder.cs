#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that sets the TableEntryReference
    /// of the component's StringReference when the bound ViewModel string value changes.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry")]
    public class LocalizeStringEventEntryMonoBinder : ComponentMonoBinder<LocalizeStringEvent, string>
    {
        /// <inheritdoc/>
        protected sealed override string Property
        {
            get => CachedComponent.StringReference.TableEntryReference.ToKeyName(this);
            set => CachedComponent.StringReference.TableEntryReference = value;
        }
    }
}
#endif