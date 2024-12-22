#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
using UnityEngine.Localization.Components;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TextLocalizationSwitcherBinder : SwitcherBinder<string>
    { 
        [Header("Component")]
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<string?, string?>? _converter;
        
        public TextLocalizationSwitcherBinder(
            string trueValue, 
            string falseValue,
            LocalizeStringEvent localizeStringEvent,
            Func<string?, string?> converter) 
            : this(trueValue, falseValue, localizeStringEvent, converter.ToConvert()) { }
        
        public TextLocalizationSwitcherBinder(
            string trueValue, 
            string falseValue,
            LocalizeStringEvent localizeStringEvent,
            IConverter<string?, string?>? converter = null) 
            : base(trueValue, falseValue)
        {
            _converter = converter;
            _localizeStringEvent = localizeStringEvent ?? throw new ArgumentNullException(nameof(localizeStringEvent));
        }

        protected override void SetValue(string value) =>
            _localizeStringEvent.StringReference.TableEntryReference = value;
    }
}
#endif