#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{CanvasGroup}"/> that sets the <see cref="CanvasGroup.interactable"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-CanvasGroup-Interactable-1.1.0.xml" path="doc//member[@name='CanvasGroupInteractableBinder']/*" />
    [Serializable]
    public class CanvasGroupInteractableBinder : TargetBoolBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.interactable;
            set => Target.interactable = value;
        }

        /// <inheritdoc/>
        public CanvasGroupInteractableBinder(CanvasGroup target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}