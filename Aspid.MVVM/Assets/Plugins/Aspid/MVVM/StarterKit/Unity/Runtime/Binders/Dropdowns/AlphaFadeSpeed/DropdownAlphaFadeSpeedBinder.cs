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
    public class DropdownAlphaFadeSpeedBinder : TargetFloatBinder<TMP_Dropdown>
    {
        protected sealed override float Property
        {
            get => Target.alphaFadeSpeed;
            set => Target.alphaFadeSpeed = value;
        }

        public DropdownAlphaFadeSpeedBinder(TMP_Dropdown target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public DropdownAlphaFadeSpeedBinder(TMP_Dropdown target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        protected override float GetConvertedValue(float value) =>
            Mathf.Max(base.GetConvertedValue(value), 0);
    }
}
#endif