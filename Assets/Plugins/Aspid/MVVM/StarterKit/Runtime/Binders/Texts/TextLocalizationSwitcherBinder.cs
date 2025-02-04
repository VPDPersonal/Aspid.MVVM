#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using UnityEngine.Localization.Components;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TextLocalizationSwitcherBinder : SwitcherBinder<LocalizeStringEvent, string>
    { 
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public TextLocalizationSwitcherBinder(
            LocalizeStringEvent target,
            string trueValue, 
            string falseValue,
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, null, mode) { }
        
        public TextLocalizationSwitcherBinder(
            LocalizeStringEvent target,
            string trueValue, 
            string falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(string value) =>
            Target.StringReference.TableEntryReference = _converter?.Convert(value) ?? value;
    }
}
#endif