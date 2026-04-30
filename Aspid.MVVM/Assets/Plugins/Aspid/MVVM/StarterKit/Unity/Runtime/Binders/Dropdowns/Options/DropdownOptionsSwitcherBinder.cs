#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TMP_Dropdown, List{TMP_Dropdown.OptionData}}"/> that switches the
    /// <see cref="TMP_Dropdown.options"/> list between two <see cref="List{T}"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Dropdown-Options-1.1.0.xml" path="doc//member[@name='DropdownOptionsSwitcherBinder']/*" />
    public sealed class DropdownOptionsSwitcherBinder : SwitcherBinder<TMP_Dropdown, List<TMP_Dropdown.OptionData>>
    {
        /// <inheritdoc/>
        public DropdownOptionsSwitcherBinder(
            TMP_Dropdown target,
            List<TMP_Dropdown.OptionData> trueValue,
            List<TMP_Dropdown.OptionData> falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(List<TMP_Dropdown.OptionData> value) =>
            Target.options = value;
    }
}
#endif