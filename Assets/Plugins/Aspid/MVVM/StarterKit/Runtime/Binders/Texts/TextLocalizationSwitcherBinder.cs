#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
using UnityEngine.Localization.Components;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TextLocalizationSwitcherBinder : SwitcherBinder<LocalizeStringEvent, string>
    { 
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<string?, string?>? _converter;
        
        public TextLocalizationSwitcherBinder(
            LocalizeStringEvent target,
            string trueValue, 
            string falseValue,
            Func<string?, string?> converter) 
            : this(target, trueValue, falseValue, converter.ToConvert()) { }
        
        public TextLocalizationSwitcherBinder(
            LocalizeStringEvent target,
            string trueValue, 
            string falseValue,
            IConverter<string?, string?>? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(string value) =>
            Target.StringReference.TableEntryReference = value;
    }
}
#endif