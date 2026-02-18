#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry")]
    public class LocalizeStringEventEntryMonoBinder : ComponentMonoBinder<LocalizeStringEvent, string, Converter>
    {
        protected sealed override string Property
        {
            get => CachedComponent.StringReference.TableEntryReference;
            set => CachedComponent.StringReference.TableEntryReference = value;
        }
    }
}
#endif