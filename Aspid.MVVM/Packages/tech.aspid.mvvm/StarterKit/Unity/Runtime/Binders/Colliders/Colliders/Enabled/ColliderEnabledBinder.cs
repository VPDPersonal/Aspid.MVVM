#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{Collider}"/> that sets the <see cref="Collider.enabled"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Collider-Enabled-1.1.0.xml" path="doc//member[@name='ColliderEnabledBinder']/*" />
    [Serializable]
    public class ColliderEnabledBinder : TargetBoolBinder<Collider>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.enabled;
            set => Target.enabled = value;
        }

        /// <inheritdoc/>
        public ColliderEnabledBinder(Collider target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}