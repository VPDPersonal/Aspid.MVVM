#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Selectable}"/> that sets the <see cref="Selectable.interactable"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Selectable-Interactable-1.1.0.xml" path="doc//member[@name='SelectableInteractableBinder']/*" />
    [Serializable]
    public class SelectableInteractableBinder : TargetBoolBinder<Selectable>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.interactable;
            set => Target.interactable = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public SelectableInteractableBinder(Selectable target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}