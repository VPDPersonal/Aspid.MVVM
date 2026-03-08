#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that sets the <see cref="CanvasGroup.ignoreParentGroups"/> property on a <see cref="CanvasGroup"/>
    /// when the bound ViewModel value changes. Supports optional value inversion.
    /// </summary>
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