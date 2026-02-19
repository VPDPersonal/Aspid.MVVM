#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class DropdownValueSwitcherBinder : SwitcherBinder<TMP_Dropdown, int, Converter>
    {
        public DropdownValueSwitcherBinder(
            TMP_Dropdown target,
            int trueValue,
            int falseValue,
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public DropdownValueSwitcherBinder(
            TMP_Dropdown target,
            int trueValue,
            int falseValue, 
            Converter? converter,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(int value) =>
            Target.value = value;
    }
}
#endif