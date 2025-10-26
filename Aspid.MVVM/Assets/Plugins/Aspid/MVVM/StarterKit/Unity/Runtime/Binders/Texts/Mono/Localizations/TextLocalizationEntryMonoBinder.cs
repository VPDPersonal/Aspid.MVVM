#if (UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION) && ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using System.Collections.Generic;
using Object = UnityEngine.Object;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - Localization Entry")]
    [AddPropertyContextMenu(typeof(TMP_Text), "m_text")]
    [AddComponentContextMenu(typeof(TMP_Text),"Add Text Binder/Text Binder - Localization Entry")]
    public partial class TextLocalizationEntryMonoBinder : ComponentMonoBinder<TMP_Text>, IBinder<string>
    {
        [SerializeField] private LocalizedString _stringReference = new();
        [SerializeField] private List<Object> _formatArguments = new();
        
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
        
        [BinderLog]
        public void SetValue(string value) =>
            _stringReference.TableEntryReference = _converter?.Convert(value) ?? value;

        protected virtual void UpdateString(string value) =>
            CachedComponent.text = value;
    }
}
#endif