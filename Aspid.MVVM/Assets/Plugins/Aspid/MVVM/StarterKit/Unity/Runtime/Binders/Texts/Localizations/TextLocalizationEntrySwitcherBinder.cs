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
    public class TextLocalizationEntrySwitcherBinder : SwitcherBinder<TMP_Text, string, Converter>
    {
        [SerializeField] private LocalizedString _stringReference = new();
        [SerializeField] private List<Object> _formatArguments = new();

        public TextLocalizationEntrySwitcherBinder(
            TMP_Text target,
            string trueValue, 
            string falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public TextLocalizationEntrySwitcherBinder(
            TMP_Text target, 
            string trueValue, 
            string falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, entry: null, formatArguments: null, converter, mode) { }
        
        public TextLocalizationEntrySwitcherBinder(
            TMP_Text target, 
            string trueValue, 
            string falseValue,
            List<Object>? formatArguments,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, entry: null, formatArguments, converter, mode) { }
        
        public TextLocalizationEntrySwitcherBinder(
            TMP_Text target, 
            string trueValue, 
            string falseValue,
            string? entry,
            List<Object>? formatArguments = null,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            mode.ThrowExceptionIfTwo();
            
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
        
        protected override void SetValue(string value) =>
            _stringReference.TableEntryReference = value;

        protected virtual void UpdateString(string value) =>
            Target.text = value;
    }
}
#endif