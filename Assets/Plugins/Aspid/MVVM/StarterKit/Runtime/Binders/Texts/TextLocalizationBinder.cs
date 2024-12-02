#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using Aspid.MVVM.StarterKit.Converters;
using UnityEngine.Localization.Components;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class TextLocalizationBinder : Binder, IBinder<string>
    {
        private readonly LocalizeStringEvent _localizeStringEvent;
        private readonly IConverter<string, string>? _converter;

        public TextLocalizationBinder(LocalizeStringEvent localizeStringEvent, Func<string, string> converter)
            : this(localizeStringEvent, new GenericFuncConverter<string, string>(converter)) { }
        
        public TextLocalizationBinder(LocalizeStringEvent localizeStringEvent, IConverter<string, string>? converter = null)
        {
            _converter = converter;
            _localizeStringEvent = localizeStringEvent ?? throw new ArgumentNullException(nameof(localizeStringEvent));
        }
        
        public void SetValue(string value) =>
            _localizeStringEvent.StringReference.TableEntryReference = _converter?.Convert(value) ?? value;
    }
}
#endif