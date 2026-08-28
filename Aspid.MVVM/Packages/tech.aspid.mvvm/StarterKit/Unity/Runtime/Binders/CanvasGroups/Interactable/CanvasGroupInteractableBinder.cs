#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{CanvasGroup, bool}"/> that sets the <see cref="CanvasGroup.interactable"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-CanvasGroup-Interactable-1.1.0.xml" path="doc//member[@name='CanvasGroupInteractableBinder']/*" />
    [Serializable]
    public class CanvasGroupInteractableBinder : TargetBinder<CanvasGroup, bool>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public CanvasGroupInteractableBinder(CanvasGroup target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneTime)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.interactable;
            set => Target.interactable = value;
        }
    }
}