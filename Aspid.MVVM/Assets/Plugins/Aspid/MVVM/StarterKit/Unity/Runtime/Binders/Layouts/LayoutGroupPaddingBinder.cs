#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class LayoutGroupPaddingBinder : TargetBinder<LayoutGroup>, IBinder<RectOffset>
    {
        [SerializeField] private PaddingMode _paddingMode;
        
        public LayoutGroupPaddingBinder(
            LayoutGroup target, 
            PaddingMode paddingMode,
            BindMode bindMode = BindMode.OneWay)
            : base(target, bindMode)
        {
            bindMode.ThrowExceptionIfTwo();
            _paddingMode = paddingMode;
        }

        public void SetValue(RectOffset? value)
        {
            if (value == null) return;
            Target.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
        }
    }
}