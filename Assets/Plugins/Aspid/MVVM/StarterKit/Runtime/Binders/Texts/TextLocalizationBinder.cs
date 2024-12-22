#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
using UnityEngine.Localization.Components;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TextLocalizationBinder : Binder, IBinder<string>
    {
        [Header("Component")]
        private LocalizeStringEvent _localizeStringEvent;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<string?, string?>? _converter;

        public TextLocalizationBinder(LocalizeStringEvent localizeStringEvent, Func<string?, string?> converter)
            : this(localizeStringEvent, converter.ToConvert()) { }
        
        public TextLocalizationBinder(LocalizeStringEvent localizeStringEvent, IConverter<string?, string?>? converter = null)
        {
            _converter = converter;
            _localizeStringEvent = localizeStringEvent ?? throw new ArgumentNullException(nameof(localizeStringEvent));
        }
        
        public void SetValue(string? value) =>
            _localizeStringEvent.StringReference.TableEntryReference = _converter?.Convert(value) ?? value;
    }
}
#endif