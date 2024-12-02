#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine.Localization.Components;

namespace Aspid.MVVM.StarterKit.Binders
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