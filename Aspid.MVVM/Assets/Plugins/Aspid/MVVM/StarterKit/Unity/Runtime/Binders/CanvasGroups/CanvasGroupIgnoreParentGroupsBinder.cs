#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class CanvasGroupIgnoreParentGroupsBinder : TargetBoolBinder<CanvasGroup>
    {
        protected override bool Property
        {
            get => Target.ignoreParentGroups;
            set => Target.ignoreParentGroups = value;
        }

        public CanvasGroupIgnoreParentGroupsBinder(CanvasGroup target, BindMode mode)
            : this(target, isInvert: false, mode) { }
        
        public CanvasGroupIgnoreParentGroupsBinder(CanvasGroup target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}