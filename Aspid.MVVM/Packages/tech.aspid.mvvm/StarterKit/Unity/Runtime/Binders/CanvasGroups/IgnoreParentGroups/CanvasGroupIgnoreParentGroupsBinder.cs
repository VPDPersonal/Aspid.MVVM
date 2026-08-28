#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{CanvasGroup, bool}"/> that sets the <see cref="CanvasGroup.ignoreParentGroups"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-CanvasGroup-IgnoreParentGroups-1.1.0.xml" path="doc//member[@name='CanvasGroupIgnoreParentGroupsBinder']/*" />
    [Serializable]
    public sealed class CanvasGroupIgnoreParentGroupsBinder : TargetBinder<CanvasGroup, bool>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public CanvasGroupIgnoreParentGroupsBinder(CanvasGroup target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneTime)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected override bool Property
        {
            get => Target.ignoreParentGroups;
            set => Target.ignoreParentGroups = value;
        }
    }
}