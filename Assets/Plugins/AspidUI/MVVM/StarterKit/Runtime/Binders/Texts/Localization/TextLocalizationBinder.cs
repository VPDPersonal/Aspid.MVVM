#if ASPID_UI_UNITY_LOCALIZATION_INTEGRATION
using System;
using AspidUI.MVVM.StarterKit.Converters;
using UnityEngine.Localization.Components;

namespace AspidUI.MVVM.StarterKit.Binders.Texts.Localization
{
    public class TextLocalizationBinder : Binder, IBinder<string>
    {
        protected readonly LocalizeStringEvent LocalizeStringEvent;
        protected readonly IConverter<string, string> Converter;

        public TextLocalizationBinder(LocalizeStringEvent localizeStringEvent, Func<string, string> converter)
            : this(localizeStringEvent, new GenericFuncConverter<string, string>(converter)) { }
        
        public TextLocalizationBinder(LocalizeStringEvent localizeStringEvent, IConverter<string, string> converter = null)
        {
            Converter = converter;
            LocalizeStringEvent = localizeStringEvent;
        }
        
        public void SetValue(string value) =>
            LocalizeStringEvent.StringReference.TableEntryReference = Converter?.Convert(value) ?? value;
    }
}
#endif