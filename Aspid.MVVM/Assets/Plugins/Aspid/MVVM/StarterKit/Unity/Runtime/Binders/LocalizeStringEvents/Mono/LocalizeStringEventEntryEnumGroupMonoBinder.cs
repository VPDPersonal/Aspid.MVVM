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
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry EnumGroup")]
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference", SubPath = "EnumGroup")]
    public sealed class LocalizeStringEventEntryEnumGroupMonoBinder : EnumGroupMonoBinder<LocalizeStringEvent, string, Converter>
    {
        protected override void SetValue(LocalizeStringEvent element, string value) =>
            element.StringReference.TableEntryReference = value;
    }
}
#endif