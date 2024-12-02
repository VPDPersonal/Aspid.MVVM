#if ASPID_UI_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine.Localization.Components;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public sealed class TextLocalizationSwitcherBinder : SwitcherBinder<string>
    { 
        private readonly LocalizeStringEvent _localizeStringEvent;
        
        public TextLocalizationSwitcherBinder(LocalizeStringEvent localizeStringEvent, string trueValue, string falseValue) 
            : base(trueValue, falseValue)
        {
            _localizeStringEvent = localizeStringEvent ?? throw new ArgumentNullException(nameof(localizeStringEvent));
        }

        protected override void SetValue(string value) =>
            _localizeStringEvent.StringReference.TableEntryReference = value;
    }
}
#endif