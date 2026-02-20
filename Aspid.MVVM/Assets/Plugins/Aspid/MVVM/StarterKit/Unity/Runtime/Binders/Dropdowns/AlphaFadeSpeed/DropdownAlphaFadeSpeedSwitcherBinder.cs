#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class DropdownAlphaFadeSpeedSwitcherBinder : SwitcherBinder<TMP_Dropdown, float, Converter>
    {
        public DropdownAlphaFadeSpeedSwitcherBinder(
            TMP_Dropdown target,
            float trueValue, 
            float falseValue,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public DropdownAlphaFadeSpeedSwitcherBinder(
            TMP_Dropdown target,
            float trueValue, 
            float falseValue,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(float value) => 
            Target.alphaFadeSpeed = value;
        
        protected override float GetConvertedValue(float value) =>
            Mathf.Max(base.GetConvertedValue(value), 0);
    }
}
#endif