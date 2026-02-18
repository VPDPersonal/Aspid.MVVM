#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class CanvasGroupInteractableBinder : TargetBoolBinder<CanvasGroup>
    {
        protected sealed override bool Property
        {
            get => Target.interactable;
            set => Target.interactable = value;
        }

        public CanvasGroupInteractableBinder(CanvasGroup target, BindMode mode)
            : this(target, isInvert: false, mode) { }
        
        public CanvasGroupInteractableBinder(CanvasGroup target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}