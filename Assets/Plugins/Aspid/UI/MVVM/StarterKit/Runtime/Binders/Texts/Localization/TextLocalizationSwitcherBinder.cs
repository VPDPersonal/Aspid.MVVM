#if ASPID_UI_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine.Localization.Components;

namespace Aspid.UI.MVVM.StarterKit.Binders.Texts.Localization
{
    public sealed class TextLocalizationSwitcherBinder : SwitcherBinder<string>
    { 
        private readonly LocalizeStringEvent _localizeStringEvent;
        
        public TextLocalizationSwitcherBinder(LocalizeStringEvent localizeStringEvent, string trueValue, string falseValue) 
            : base(trueValue, falseValue)
        {
            _localizeStringEvent = localizeStringEvent;
        }

        protected override void SetValue(string value) =>
            _localizeStringEvent.StringReference.TableEntryReference = value;
    }
}
#endif