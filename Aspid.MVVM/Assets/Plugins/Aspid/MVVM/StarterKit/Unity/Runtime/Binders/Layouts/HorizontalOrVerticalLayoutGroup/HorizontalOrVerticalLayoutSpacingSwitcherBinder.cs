#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class HorizontalOrVerticalLayoutSpacingSwitcherBinder : SwitcherBinder<HorizontalOrVerticalLayoutGroup, float>
    {
        public HorizontalOrVerticalLayoutSpacingSwitcherBinder(
            HorizontalOrVerticalLayoutGroup target,
            float trueValue, 
            float falseValue,
            BindMode bindMode = BindMode.OneWay)
            : base(target, trueValue, falseValue, bindMode) { }

        protected override void SetValue(float value) =>
            Target.spacing = value;
    }
}