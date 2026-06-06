#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{CanvasGroup}"/> that sets the <see cref="CanvasGroup.ignoreParentGroups"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-CanvasGroup-IgnoreParentGroups-1.1.0.xml" path="doc//member[@name='CanvasGroupIgnoreParentGroupsBinder']/*" />
    [Serializable]
    public sealed class CanvasGroupIgnoreParentGroupsBinder : TargetBoolBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected override bool Property
        {
            get => Target.ignoreParentGroups;
            set => Target.ignoreParentGroups = value;
        }

        /// <inheritdoc/>
        public CanvasGroupIgnoreParentGroupsBinder(CanvasGroup target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}