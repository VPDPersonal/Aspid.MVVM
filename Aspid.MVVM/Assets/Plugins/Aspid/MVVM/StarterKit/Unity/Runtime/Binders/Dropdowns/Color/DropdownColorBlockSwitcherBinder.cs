#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.UI.ColorBlock, UnityEngine.UI.ColorBlock>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColorBlock;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class DropdownColorBlockSwitcherBinder : SwitcherBinder<TMP_Dropdown, ColorBlock, Converter>
    {
        public DropdownColorBlockSwitcherBinder(
            TMP_Dropdown target, 
            ColorBlock trueValue, 
            ColorBlock falseValue,
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, converter: null, mode) { }

        public DropdownColorBlockSwitcherBinder(
            TMP_Dropdown target, 
            ColorBlock trueValue, 
            ColorBlock falseValue,
            Converter? converter,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(ColorBlock value) =>
            Target.colors = value;
    }
}
#endif