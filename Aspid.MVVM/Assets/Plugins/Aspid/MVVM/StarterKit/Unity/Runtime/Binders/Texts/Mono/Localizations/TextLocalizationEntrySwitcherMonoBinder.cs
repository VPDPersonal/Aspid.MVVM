#if (UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION) && ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using System.Collections.Generic;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames:"m_text")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Localization Entry Switcher")]
    public class TextLocalizationEntrySwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, string>
    {
        [SerializeField] private LocalizedString _stringReference = new();
        [SerializeField] private List<Object> _formatArguments = new();
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void OnValidate()
        {
            base.OnValidate();
            _stringReference?.RefreshString();
        }
        
        protected virtual void OnEnable() =>
            Subscribe();

        protected virtual void OnDisable() =>
            Unsubscribe();

        private void Subscribe() =>
            _stringReference.Subscribe(_formatArguments, UpdateString);

        private void Unsubscribe() =>
            _stringReference.Unsubscribe(UpdateString);
        
        protected override void SetValue(string value)  =>
            _stringReference.TableEntryReference = _converter?.Convert(value) ?? value;
        
        protected virtual void UpdateString(string value) =>
            CachedComponent.text = value;
    }
}
#endif