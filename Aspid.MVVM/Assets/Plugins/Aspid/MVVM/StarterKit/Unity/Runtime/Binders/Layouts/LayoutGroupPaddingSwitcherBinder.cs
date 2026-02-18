#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.RectOffset?, UnityEngine.RectOffset?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterRectOffset;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class LayoutGroupPaddingSwitcherBinder : SwitcherBinder<LayoutGroup, RectOffset, Converter>
    {
        [SerializeField] private PaddingMode _paddingMode;
        
        public LayoutGroupPaddingSwitcherBinder(
            LayoutGroup target,
            RectOffset trueValue, 
            RectOffset falseValue,
            PaddingMode paddingMode,
            BindMode bindMode = BindMode.OneWay)
            : this(target, trueValue, falseValue, paddingMode, null, bindMode) { }
        
        public LayoutGroupPaddingSwitcherBinder(
            LayoutGroup target,
            RectOffset trueValue, 
            RectOffset falseValue,
            PaddingMode paddingMode,
            Converter? converter = null,
            BindMode bindMode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, bindMode)
        {
            _paddingMode = paddingMode;
        }

        protected override void SetValue(RectOffset value) =>
            Target.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}