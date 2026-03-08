#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that sets the <see cref="CanvasGroup.blocksRaycasts"/> property on a <see cref="CanvasGroup"/>
    /// when the bound ViewModel value changes. Supports optional value inversion.
    /// </summary>
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