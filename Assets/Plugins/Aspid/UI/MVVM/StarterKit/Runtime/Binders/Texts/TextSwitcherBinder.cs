#if UNITY_2023_1_OR_NEWER || ASPID_UI_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public sealed class TextSwitcherBinder : SwitcherBinder<string>
    {
        private readonly TMP_Text _text;

        public TextSwitcherBinder(TMP_Text text, string trueValue, string falseValue) 
            : base(trueValue, falseValue)
        {
            _text = text ?? throw new ArgumentException(nameof(text));
        }

        protected override void SetValue(string value) =>
            _text.text = value;
    }
}
#endif