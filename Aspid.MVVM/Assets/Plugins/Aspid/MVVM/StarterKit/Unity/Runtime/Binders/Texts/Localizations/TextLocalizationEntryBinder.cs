#if (UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION) && ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using UnityEngine.Localization;
using System.Collections.Generic;
using Object = UnityEngine.Object;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class TextLocalizationEntryBinder : TargetBinder<TMP_Text>, IBinder<string?>
    {
        [SerializeField] private LocalizedString _stringReference = new();
        [SerializeField] private List<Object> _formatArguments = new();
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public TextLocalizationEntryBinder(TMP_Text target, BindMode mode)
            : this(target, null, mode) { }
        
        public TextLocalizationEntryBinder(
            TMP_Text target, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : this(target, null, null, converter, mode) { }
        
        public TextLocalizationEntryBinder(
            TMP_Text target, 
            List<Object>? formatArguments,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : this(target, null, formatArguments, converter, mode) { }
        
        public TextLocalizationEntryBinder(
            TMP_Text target, 
            string? entry,
            List<Object>? formatArguments = null,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _converter = converter;
            _formatArguments = formatArguments ?? _formatArguments;
            _stringReference.TableEntryReference = entry;
        }
        
        protected override void OnBinding() =>
            Subscribe();

        protected override void OnUnbound() =>
            Unsubscribe();

        private void Subscribe() =>
            _stringReference.Subscribe(_formatArguments, UpdateString);

        private void Unsubscribe() =>
            _stringReference.Unsubscribe(UpdateString);
        
        public void SetValue(string? value) =>
            _stringReference.TableEntryReference = _converter?.Convert(value) ?? value;

        protected virtual void UpdateString(string value) =>
            Target.text = value;
    }
}
#endif