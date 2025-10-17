#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public sealed class TextFontSwitcherBinder : SwitcherBinder<TMP_Text, TMP_FontAsset>
    {
        public TextFontSwitcherBinder(
            TMP_Text target, 
            TMP_FontAsset trueValue, 
            TMP_FontAsset falseValue, 
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode) { }

        protected override void SetValue(TMP_FontAsset value) =>
            Target.font = value;
    }
}
#endif