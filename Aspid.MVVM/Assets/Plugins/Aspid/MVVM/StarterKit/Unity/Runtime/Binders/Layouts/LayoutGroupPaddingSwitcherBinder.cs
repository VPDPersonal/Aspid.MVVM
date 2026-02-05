#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class LayoutGroupPaddingSwitcherBinder : SwitcherBinder<LayoutGroup, RectOffset>
    {
        [SerializeField] private PaddingMode _paddingMode;
        
        public LayoutGroupPaddingSwitcherBinder(
            LayoutGroup target,
            RectOffset trueValue, 
            RectOffset falseValue,
            PaddingMode paddingMode,
            BindMode bindMode = BindMode.OneWay)
            : base(target, trueValue, falseValue, bindMode)
        {
            _paddingMode = paddingMode;
        }

        protected override void SetValue(RectOffset value) =>
            Target.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}