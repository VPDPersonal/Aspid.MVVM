#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public sealed class DropdownOptionsSwitcherBinder : SwitcherBinder<TMP_Dropdown, List<TMP_Dropdown.OptionData>>
    {
        public DropdownOptionsSwitcherBinder(
            TMP_Dropdown target,
            List<TMP_Dropdown.OptionData> trueValue, 
            List<TMP_Dropdown.OptionData> falseValue, 
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode) { }

        protected override void SetValue(List<TMP_Dropdown.OptionData> value) =>
            Target.options = value;
    }
}
#endif