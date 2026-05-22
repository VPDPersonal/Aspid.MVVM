#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Collider}"/> that sets the <see cref="Collider.isTrigger"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Collider-IsTrigger-1.1.0.xml" path="doc//member[@name='ColliderIsTriggerBinder']/*" />
    [Serializable]
    public sealed class ColliderIsTriggerBinder : TargetBoolBinder<Collider>
    {
        /// <inheritdoc/>
        protected override bool Property
        {
            get => Target.isTrigger;
            set => Target.isTrigger = value;
        }

        /// <inheritdoc/>
        public ColliderIsTriggerBinder(Collider target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}