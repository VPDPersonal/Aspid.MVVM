#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class HorizontalOrVerticalLayoutSpacingBinder : TargetBinder<HorizontalOrVerticalLayoutGroup>, INumberBinder
    {
        public HorizontalOrVerticalLayoutSpacingBinder(
            HorizontalOrVerticalLayoutGroup target, 
            BindMode bindMode = BindMode.OneWay)
            : base(target, bindMode)
        {
            bindMode.ThrowExceptionIfTwo();
        }

        public void SetValue(int value) => 
            Target.spacing = value;

        public void SetValue(long value) =>
            SetValue((int)value);

        public void SetValue(float value) =>
            Target.spacing = value;

        public void SetValue(double value) =>
            SetValue((float)value);
    }
}

