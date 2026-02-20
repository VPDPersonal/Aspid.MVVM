#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class CanvasGroupBlocksRaycastsBinder : TargetBoolBinder<CanvasGroup>
    {
        protected sealed override bool Property
        {
            get => Target.blocksRaycasts;
            set => Target.blocksRaycasts = value;
        }

        public CanvasGroupBlocksRaycastsBinder(CanvasGroup target, BindMode mode)
            : this(target, isInvert: false, mode) { }
        
        public CanvasGroupBlocksRaycastsBinder(CanvasGroup target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}