#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{CanvasGroup}"/> that sets the <see cref="CanvasGroup.blocksRaycasts"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-CanvasGroup-BlocksRaycasts-1.1.0.xml" path="doc//member[@name='CanvasGroupBlocksRaycastsBinder']/*" />
    [Serializable]
    public class CanvasGroupBlocksRaycastsBinder : TargetBoolBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.blocksRaycasts;
            set => Target.blocksRaycasts = value;
        }

        /// <inheritdoc/>
        public CanvasGroupBlocksRaycastsBinder(CanvasGroup target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}