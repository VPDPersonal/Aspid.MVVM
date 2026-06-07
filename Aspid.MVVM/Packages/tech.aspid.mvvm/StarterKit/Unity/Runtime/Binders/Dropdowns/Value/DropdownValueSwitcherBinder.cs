#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherIntBinder{TMP_Dropdown}"/> that switches the <see cref="TMP_Dropdown.value"/>
    /// property between two <see cref="int"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Dropdown-Value-1.1.0.xml" path="doc//member[@name='DropdownValueSwitcherBinder']/*" />
    [Serializable]
    public sealed class DropdownValueSwitcherBinder : SwitcherIntBinder<TMP_Dropdown>
    {
        /// <inheritdoc/>
        public DropdownValueSwitcherBinder(
            TMP_Dropdown target,
            int trueValue,
            int falseValue,
            IConverter<int, int>? converter,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            Target.value = value;
    }
}
#endif