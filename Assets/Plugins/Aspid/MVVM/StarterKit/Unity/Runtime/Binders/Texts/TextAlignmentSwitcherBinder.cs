#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public sealed class TextAlignmentSwitcherBinder : SwitcherBinder<TMP_Text, TextAlignmentOptions>
    {
        public TextAlignmentSwitcherBinder(
            TMP_Text target,
            TextAlignmentOptions trueValue,
            TextAlignmentOptions falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode) { }

        protected override void SetValue(TextAlignmentOptions value) =>
            Target.alignment = value;
    }
}
#endif