#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Collider, bool}"/> that sets the <see cref="Collider.enabled"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Collider-Enabled-1.1.0.xml" path="doc//member[@name='ColliderEnabledBinder']/*" />
    [Serializable]
    public class ColliderEnabledBinder : TargetBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.enabled;
            set => Target.enabled = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ColliderEnabledBinder(Collider target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}